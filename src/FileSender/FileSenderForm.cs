using Compression;
using Compression.Algorithms;
using Compression.Algorithms.Huffman;
using Compression.Algorithms.RunLengthEncoding;
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

        private delegate void WritePercentageDelegate(string text);

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
            
            var file = File.ReadAllBytes(files.First());

            compressedFile = compressor.Compress(file);
            
            FileStream fileStream = new FileStream("arquivo_comprimido.dat", FileMode.Create);
            Console.WriteLine("Escrevendo arquivo");
            
            for (int position = 0; position < compressedFile.Data.Length; position++)
                fileStream.WriteByte(compressedFile.Data[position]);
            fileStream.Close();

            var compressionPercentage = (compressedFile.Data.Length * 100) / (decimal) file.Length;
            labelCompressionPercent.Text = $"{compressionPercentage.ToString("0.00")} %";

            return;

            var task = Task.Run(() =>
            {
                return compressor.Compress(file);
            });

            await Task.Run(() =>
            {
                var d = new WritePercentageDelegate(WritePercentage);

                int i = 0;

                while (!task.IsCompleted)
                {
                    if (i == 10000)
                    {
                        //labelCompressionPercent.Invoke(d, $"{huffman.Percentage} %");
                    }

                    i = (i + 1) % 10001;
                }

                this.compressedFile = task.Result;
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
            var serialized = JsonConvert.SerializeObject(compressedFile);

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

        private void comboBoxAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
