using Compression;
using Compression.Algorithms;
using Compression.Algorithms.Huffman;
using Newtonsoft.Json;
using System;
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

        private CompressedFile file;

        private delegate void WritePercentageDelegate(string text);

        public FileSenderForm()
        {
            InitializeComponent();
            comboBoxAlgorithm.SelectedIndex = 0;
        }

        private async void panelFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            var huffman = new HuffmanCoding();

            var file = File.ReadAllBytes(files.First());

            this.file = new CompressedFile(file, 0, new Compression.DataStructures.Node<byte>[0]);

            return;

            var task = Task.Run(() =>
            {
                return huffman.Compress(file);
            });

            await Task.Run(() =>
            {
                var d = new WritePercentageDelegate(WritePercentage);

                int i = 0;

                while (!task.IsCompleted)
                {
                    if (i == 10000)
                    {
                        labelCompressionPercent.Invoke(d, $"{huffman.Percentage} %");
                    }

                    i = (i + 1) % 10001;
                }

                compressedFile = task.Result;
            });

            //var compressionPercentage = compressedFile.Data.Length / (decimal)file.Length * 100;
            //labelCompressionPercent.Text = $"{compressionPercentage.ToString("0.00")} %";
        }

        private void panelFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var serialized = JsonConvert.SerializeObject(file);

            using (var content = new StringContent(serialized))
            {
                using (var client = new HttpClient())
                {
                    client.PostAsync("http://localhost:8000/api/huffman", content).Wait();
                }
            }
        }

        private void WritePercentage(string text)
        {
            labelCompressionPercent.Text = text;
        }
    }
}
