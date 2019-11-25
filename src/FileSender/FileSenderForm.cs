using System;
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
    }
}
