namespace FileSender
{
    partial class FileSenderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSenderForm));
            this.panelFile = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCompressionPercent = new System.Windows.Forms.Label();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTempo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTamanho = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panelFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFile
            // 
            this.panelFile.AllowDrop = true;
            this.panelFile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFile.Controls.Add(this.pictureBox);
            this.panelFile.Location = new System.Drawing.Point(10, 32);
            this.panelFile.Margin = new System.Windows.Forms.Padding(2);
            this.panelFile.Name = "panelFile";
            this.panelFile.Size = new System.Drawing.Size(144, 115);
            this.panelFile.TabIndex = 0;
            this.panelFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelFile_DragDrop);
            this.panelFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelFile_DragEnter);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(33, 20);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(80, 71);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Compression:";
            // 
            // labelCompressionPercent
            // 
            this.labelCompressionPercent.Location = new System.Drawing.Point(84, 149);
            this.labelCompressionPercent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCompressionPercent.Name = "labelCompressionPercent";
            this.labelCompressionPercent.Size = new System.Drawing.Size(70, 17);
            this.labelCompressionPercent.TabIndex = 3;
            this.labelCompressionPercent.Text = "--%";
            this.labelCompressionPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlgorithm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxAlgorithm.FormattingEnabled = true;
            this.comboBoxAlgorithm.IntegralHeight = false;
            this.comboBoxAlgorithm.Items.AddRange(new object[] {
            "Huffman",
            "RLE"});
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(10, 7);
            this.comboBoxAlgorithm.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(144, 21);
            this.comboBoxAlgorithm.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(10, 207);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(84, 207);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(70, 23);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoEllipsis = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(8, 234);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(144, 12);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Status: -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tempo:";
            // 
            // labelTempo
            // 
            this.labelTempo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTempo.AutoSize = true;
            this.labelTempo.Location = new System.Drawing.Point(80, 169);
            this.labelTempo.Name = "labelTempo";
            this.labelTempo.Size = new System.Drawing.Size(76, 13);
            this.labelTempo.TabIndex = 8;
            this.labelTempo.Text = "00:00:00.0000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tamanho:";
            // 
            // labelTamanho
            // 
            this.labelTamanho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTamanho.AutoEllipsis = true;
            this.labelTamanho.Location = new System.Drawing.Point(68, 187);
            this.labelTamanho.Name = "labelTamanho";
            this.labelTamanho.Size = new System.Drawing.Size(88, 18);
            this.labelTamanho.TabIndex = 10;
            this.labelTamanho.Text = "0 kb";
            this.labelTamanho.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FileSenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(161, 255);
            this.Controls.Add(this.labelTamanho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelTempo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelFile);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxAlgorithm);
            this.Controls.Add(this.labelCompressionPercent);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileSenderForm";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "File Compressor";
            this.panelFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelCompressionPercent;
        private System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTempo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTamanho;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}
