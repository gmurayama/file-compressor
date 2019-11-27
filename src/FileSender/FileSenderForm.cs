using Compression;
using Compression.Algorithms;
using Compression.Algorithms.Huffman;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileSender
{
    public partial class FileSenderForm : Form
    {
        private string[] files;

        public FileSenderForm()
        {
            InitializeComponent();
            comboBoxAlgorithm.SelectedIndex = 0;
        }

        private void panelFile_DragDrop(object sender, DragEventArgs e)
        {
            files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            var huffman = new HuffmanCoding();

            var file = File.ReadAllBytes(files.First());

            var compressed = huffman.Compress(file);
            var compressionPercentage = compressed.Data.Length / (decimal)file.Length * 100;
            labelCompressionPercent.Text = $"{compressionPercentage.ToString("0.00")} %";
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

        }

        private void labelCompressionPercent_Click(object sender, EventArgs e)
        {

        }
    }
}
