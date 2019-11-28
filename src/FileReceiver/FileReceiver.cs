using Compression.Algorithms.Huffman;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileReceiver
{
    public partial class FileReceiver : Form
    {
        private Thread serverThread;
        private Server server;


        private delegate void PaintImageDelegate(CompressedFile file);

        public FileReceiver()
        {
            InitializeComponent();
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

                    if (request.Url.AbsolutePath == "/api/huffman")
                    {
                        string body;

                        using (var reader = new StreamReader(request.InputStream,
                                                             request.ContentEncoding))
                        {
                            body = reader.ReadToEnd();
                        }

                        var compressed = JsonConvert.DeserializeObject<CompressedFile>(body);

                        var d = new PaintImageDelegate(PaintImage);
                        pictureBoxImage.Invoke(d, compressed);
                    }
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

        private void PaintImage(CompressedFile file)
        {
            //var huffman = new HuffmanCoding();
            //var data = huffman.Decompress(file);

            using (var ms = new MemoryStream(file.Data))
            {
                pictureBoxImage.Image = Image.FromStream(ms);
            }
        }
    }
}
