using Compression.Algorithms;
using Compression.Algorithms.Huffman;
using Compression.Algorithms.RunLengthEncoding;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileReceiver
{
    public partial class FileReceiver : Form
    {
        private Thread serverThread;
        private Server server;

        private CompressedFile file;

        public static readonly string[] imageExtensions = new string[] { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        private delegate void PaintImageDelegate();
        private delegate void WriteStatusDelegate(string text);
        private delegate void SetButtonSalvarEnabledDelegate(bool enabled);

        public FileReceiver()
        {
            InitializeComponent();
            buttonSalvar.Enabled = false;
        }

        private void FileReceiver_Load(object sender, EventArgs e)
        {
            server = new Server();

            serverThread = new Thread(async () =>
            {
                server.Start();

                while (!server.IsStopped)
                {
                    var context = await server.Listening();
                    var request = context.Request;

                    var setEnabled = new SetButtonSalvarEnabledDelegate(SetButtonSalvarEnabled);
                    buttonSalvar.Invoke(setEnabled, false);

                    if (request.Url.AbsolutePath == "/" && request.HttpMethod == "POST")
                    {
                        var writeStatus = new WriteStatusDelegate(WriteStatus);

                        string body;

                        labelStatus.Invoke(writeStatus, "Status: Lendo corpo da requisição");

                        using (var reader = new StreamReader(request.InputStream,
                                                             request.ContentEncoding))
                        {
                            body = reader.ReadToEnd();
                        }

                        labelStatus.Invoke(writeStatus, "Status: Lido corpo da requisição");

                        file = JsonConvert.DeserializeObject<CompressedFile>(body);

                        if (imageExtensions.Contains(Path.GetExtension(file.Name.ToUpperInvariant())))
                        {
                            var d = new PaintImageDelegate(PaintImage);
                            pictureBoxImage.Invoke(d);
                        }

                        labelStatus.Invoke(writeStatus, "Status: Ações completadas com sucesso");
                        buttonSalvar.Invoke(setEnabled, true);
                    }

                    var response = context.Response;
                    response.ContentLength64 = 0;
                    response.StatusCode = 200;
                    response.Close();
                }

                server.Stop();
            });

            serverThread.Start();
        }

        private void FileReceiver_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
            serverThread.Join(1000 * 5);
        }

        public byte[] DecompressFile(CompressedFile compressedFile)
        {
            ICompressor compressor;

            if (compressedFile.Queue != null)
            {
                compressor = new HuffmanCoding();
            }
            else
            {
                compressor = new RunLengthEncodingCompressor();
            }

            return compressor.Decompress(compressedFile);
        }

        private void PaintImage()
        {
            var writeStatus = new WriteStatusDelegate(WriteStatus);

            labelStatus.Invoke(writeStatus, "Status: Descomprimindo imagens...");

            var fileData = DecompressFile(file);

            labelStatus.Invoke(writeStatus, "Status: Imagem descomprimida com sucesso");

            using (var ms = new MemoryStream(fileData))
            {
                pictureBoxImage.Image = Image.FromStream(ms);
            }
        }

        private void WriteStatus(string text)
        {
            labelStatus.Text = text;
        }

        private void SetButtonSalvarEnabled(bool enabled)
        {
            buttonSalvar.Enabled = enabled;
        }

        private async void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var writeStatus = new WriteStatusDelegate(WriteStatus);

                labelStatus.Invoke(writeStatus, "Status: Descomprimindo arquivo...");

                var fileData = await Task.Run(() => DecompressFile(file));

                labelStatus.Invoke(writeStatus, "Status: Arquivo descomprimido com sucesso");

                var path = @folderBrowserDialog.SelectedPath;
                File.WriteAllBytes(@Path.Combine(path, file.Name), fileData);

                labelStatus.Invoke(writeStatus, "Status: Arquivo salvo com sucesso");
            } 
        }
    }
}
