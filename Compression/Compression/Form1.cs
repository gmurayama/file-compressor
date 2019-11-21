using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Compression
{
    
    
    
    public partial class Form1 : Form
    {
        string FilePath = null;
        string DefaultSubject =  Email.GetDayMonth() + " - Documentos Médicos" ;
        string DefaultBody = Email.Cumpliment() + "\n\nSegue em anexo documentos médicos comprimidos por File Compressor\n\nAtt,\n\n" + System.Environment.UserName;
        string CompressedFileExtension = ".nabo";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            /* Highlights panel */
            panel1.BackColor = SystemColors.ControlLight;

            /* Get Dragged Filepaths */
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            /*If only a single file is dragged */
            if (files.Length == 1)
            {
                FilePath = files[0];
                byte[] FileIn, FileOut;

                label3.Text = System.IO.Path.GetFileName(FilePath);

                /* If file is compressed */
                if (System.IO.Path.GetExtension(FilePath) == CompressedFileExtension)
                {
                    System.IO.File.WriteAllBytes(System.IO.Path.GetFileNameWithoutExtension(FilePath) + CompressedFileExtension, Compressor.Decompress(System.IO.File.ReadAllBytes(FilePath)) );
                }
                else
                {
                    /* Read file bytes */
                    FileIn = System.IO.File.ReadAllBytes(FilePath);

                    /* Compress bytes */
                    FileOut = Compressor.Compress(FileIn, numericUpDown1.Value, (CompressionType)comboBox1.SelectedIndex);

                    /* Get Compress rate */
                    label2.Text = ((1 - (FileOut.LongLength / FileIn.LongLength)) * 100).ToString() + '%';

                    /* Create Compressed File */
                    System.IO.File.WriteAllBytes(System.IO.Path);
                }
                



            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if ( ((string[]) e.Data.GetData(DataFormats.FileDrop)).Length > 1)
                this.Cursor = Cursors.No;
            else
            {
                panel1.BackColor = SystemColors.ControlLightLight;
                e.Effect = DragDropEffects.Copy;
            }
               
            
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            panel1.BackColor = SystemColors.ControlLight;
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(FilePath != null)
            {
                Email.SendMail(DefaultSubject , DefaultBody, FilePath);
            }
            
        }
    }
}
