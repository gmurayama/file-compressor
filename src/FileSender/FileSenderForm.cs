using Compression;
using Compression.Algorithms;
using Compression.Algorithms.Huffman;
using Compression.Algorithms.RunLengthEncoding;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSender
{
    public partial class FileSenderForm : Form
    {
        private CompressedFile compressedFile;
        
        private delegate void WriteStatusDelegate(string text);

        public FileSenderForm()
        {
            InitializeComponent();
            comboBoxAlgorithm.SelectedIndex = 0;
        }

        private async void panelFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            ICompressor compressor;

            if (comboBoxAlgorithm.Text.Equals("Huffman"))
            {
                compressor = new HuffmanCoding();
            } else
            {
                compressor = new RunLengthEncodingCompressor();
            }
            
            var file = File.ReadAllBytes(@files.First());

            try
            {
                pictureBox.Hide();
                panelFile.BackgroundImage = Image.FromFile(@files.First());
            }
            catch
            {
                pictureBox.Show();
                panelFile.BackgroundImage = null;
            }

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            var task = Task.Run(() =>
            {
                return compressor.Compress(file);
            });

            compressedFile = await Task.Run(() =>
            {
                var writeStatus = new WriteStatusDelegate(WriteStatus);

                int i = 0;

                while (!task.IsCompleted)
                {
                    if (i == 500)
                    {
                        labelStatus.Invoke(writeStatus, $"Status: {compressor.Percentage.ToString("0.00")} % processado");
                    }

                    i = (i + 1) % 501;
                }

                labelStatus.Invoke(writeStatus, $"Status: 100 % processado");

                var f = task.Result;
                f.Name = Path.GetFileName(files.First());
                return f;
            });

            stopWatch.Stop();

            var compressionPercentage = compressedFile.Data.Length * 100M / file.Length;
            labelTempo.Text = stopWatch.Elapsed.ToString(@"hh\:mm\:ss\.ffff");
            labelTamanho.Text = (compressedFile.Data.LongLength / 1024M).ToString("0.000") + " kb";
            labelCompressionPercent.Text = $"{compressionPercentage.ToString("0.00")} %";
        }

        private void panelFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var path = @Path.Combine(folderBrowserDialog.SelectedPath, $"{compressedFile.Name}.dat");
                var fileStream = new FileStream(path, FileMode.Create);
                var d = new WriteStatusDelegate(WriteStatus);

                await Task.Run(() =>
                {
                    for (int position = 0; position < compressedFile.Data.Length; position++)
                    {
                        fileStream.WriteByte(compressedFile.Data[position]);

                        if (position % 500 == 0)
                            labelStatus.Invoke(d, $"Salvando: {(position * 100M / compressedFile.Data.Length).ToString("0.00")} %");
                    }

                    labelStatus.Invoke(d, $"Salvando: 100 %");

                    return Task.CompletedTask;
                });

                fileStream.Close();
            }
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            var serialized = JsonConvert.SerializeObject(compressedFile);

            using (var content = new StringContent(serialized))
            {
                using (var client = new HttpClient())
                {
                    labelStatus.Text = "Status: Enviando arquivo...";

                    try
                    {
                        var response = await client.PostAsync("http://localhost:8000/", content);

                        if (response.IsSuccessStatusCode)
                            labelStatus.Text = "Status: Arquivo enviado";
                        else
                            labelStatus.Text = $"Status: Erro ao enviar arquivo! (StatusCode {response.StatusCode})";
                    }
                    catch (Exception ex)
                    {
                        labelStatus.Text = $"Status: Erro ao enviar arquivo! (ErrorCode {ex.HResult})";
                    }
                }
            }
        }

        private void WriteStatus(string text)
        {
            labelStatus.Text = text;
        }
    }
}
