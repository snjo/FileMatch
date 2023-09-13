namespace FileMatch
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonSelectFolder1 = new Button();
            ColumnFile1 = new ColumnHeader();
            ColumnDeleted1 = new ColumnHeader();
            ColumnMatch1 = new ColumnHeader();
            buttonSelectFolder2 = new Button();
            pictureBox1 = new PictureBox();
            buttonDelete1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonCompare = new Button();
            panelForPicture = new Panel();
            buttonZoom100 = new Button();
            buttonZoomFit = new Button();
            buttonZoomPlus = new Button();
            buttonZoomMinus = new Button();
            ColumnFile2 = new ColumnHeader();
            ColumnDeleted2 = new ColumnHeader();
            ColumnMatch2 = new ColumnHeader();
            timerMarkMatches = new System.Windows.Forms.Timer(components);
            labelZoom = new Label();
            checkBoxLoadPictureOnSingleClick = new CheckBox();
            buttonClear1 = new Button();
            button1 = new Button();
            FileGrid = new DataGridView();
            ColumnFileName1 = new DataGridViewTextBoxColumn();
            ColumnThumbnail1 = new DataGridViewImageColumn();
            ColumnFileName2 = new DataGridViewTextBoxColumn();
            Delete1 = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelForPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FileGrid).BeginInit();
            SuspendLayout();
            // 
            // buttonSelectFolder1
            // 
            buttonSelectFolder1.Location = new Point(12, 12);
            buttonSelectFolder1.Name = "buttonSelectFolder1";
            buttonSelectFolder1.Size = new Size(107, 23);
            buttonSelectFolder1.TabIndex = 0;
            buttonSelectFolder1.Text = "Select Folder 1";
            buttonSelectFolder1.UseVisualStyleBackColor = true;
            buttonSelectFolder1.Click += buttonSelectFolder1_Click;
            // 
            // ColumnFile1
            // 
            ColumnFile1.Text = "File Name";
            ColumnFile1.Width = 170;
            // 
            // ColumnDeleted1
            // 
            ColumnDeleted1.Text = "Del";
            ColumnDeleted1.Width = 35;
            // 
            // ColumnMatch1
            // 
            ColumnMatch1.Text = "Match";
            ColumnMatch1.Width = 55;
            // 
            // buttonSelectFolder2
            // 
            buttonSelectFolder2.Location = new Point(290, 12);
            buttonSelectFolder2.Name = "buttonSelectFolder2";
            buttonSelectFolder2.Size = new Size(107, 23);
            buttonSelectFolder2.TabIndex = 2;
            buttonSelectFolder2.Text = "Select Folder 2";
            buttonSelectFolder2.UseVisualStyleBackColor = true;
            buttonSelectFolder2.Click += buttonSelectFolder2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(908, 632);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.LoadCompleted += pictureBox1_LoadCompleted;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            // 
            // buttonDelete1
            // 
            buttonDelete1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDelete1.Location = new Point(125, 682);
            buttonDelete1.Name = "buttonDelete1";
            buttonDelete1.Size = new Size(107, 23);
            buttonDelete1.TabIndex = 5;
            buttonDelete1.Text = "Delete Selected";
            buttonDelete1.UseVisualStyleBackColor = true;
            buttonDelete1.Click += buttonDelete_Click;
            // 
            // buttonCompare
            // 
            buttonCompare.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonCompare.Location = new Point(12, 682);
            buttonCompare.Name = "buttonCompare";
            buttonCompare.Size = new Size(107, 23);
            buttonCompare.TabIndex = 6;
            buttonCompare.Text = "Compare";
            buttonCompare.UseVisualStyleBackColor = true;
            buttonCompare.Click += buttonCompare_Click;
            // 
            // panelForPicture
            // 
            panelForPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelForPicture.AutoScroll = true;
            panelForPicture.BackColor = Color.Black;
            panelForPicture.Controls.Add(pictureBox1);
            panelForPicture.Location = new Point(497, 41);
            panelForPicture.Name = "panelForPicture";
            panelForPicture.Size = new Size(914, 638);
            panelForPicture.TabIndex = 14;
            // 
            // buttonZoom100
            // 
            buttonZoom100.Location = new Point(500, 12);
            buttonZoom100.Name = "buttonZoom100";
            buttonZoom100.Size = new Size(53, 23);
            buttonZoom100.TabIndex = 15;
            buttonZoom100.Text = "100%";
            buttonZoom100.UseVisualStyleBackColor = true;
            buttonZoom100.Click += buttonZoom100_Click;
            // 
            // buttonZoomFit
            // 
            buttonZoomFit.Location = new Point(559, 12);
            buttonZoomFit.Name = "buttonZoomFit";
            buttonZoomFit.Size = new Size(39, 23);
            buttonZoomFit.TabIndex = 16;
            buttonZoomFit.Text = "Fit";
            buttonZoomFit.UseVisualStyleBackColor = true;
            buttonZoomFit.Click += buttonZoomFit_Click;
            // 
            // buttonZoomPlus
            // 
            buttonZoomPlus.Location = new Point(604, 12);
            buttonZoomPlus.Name = "buttonZoomPlus";
            buttonZoomPlus.Size = new Size(39, 23);
            buttonZoomPlus.TabIndex = 17;
            buttonZoomPlus.Text = "+";
            buttonZoomPlus.UseVisualStyleBackColor = true;
            buttonZoomPlus.Click += buttonZoomPlus_Click;
            // 
            // buttonZoomMinus
            // 
            buttonZoomMinus.Location = new Point(649, 12);
            buttonZoomMinus.Name = "buttonZoomMinus";
            buttonZoomMinus.Size = new Size(39, 23);
            buttonZoomMinus.TabIndex = 18;
            buttonZoomMinus.Text = "-";
            buttonZoomMinus.UseVisualStyleBackColor = true;
            buttonZoomMinus.Click += buttonZoomMinus_Click;
            // 
            // ColumnFile2
            // 
            ColumnFile2.Text = "File Name";
            ColumnFile2.Width = 170;
            // 
            // ColumnDeleted2
            // 
            ColumnDeleted2.Text = "Del";
            ColumnDeleted2.Width = 35;
            // 
            // ColumnMatch2
            // 
            ColumnMatch2.Text = "Match";
            ColumnMatch2.Width = 55;
            // 
            // timerMarkMatches
            // 
            timerMarkMatches.Tick += timer1_Tick;
            // 
            // labelZoom
            // 
            labelZoom.AutoSize = true;
            labelZoom.Location = new Point(703, 16);
            labelZoom.Name = "labelZoom";
            labelZoom.Size = new Size(39, 15);
            labelZoom.TabIndex = 21;
            labelZoom.Text = "Zoom";
            // 
            // checkBoxLoadPictureOnSingleClick
            // 
            checkBoxLoadPictureOnSingleClick.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxLoadPictureOnSingleClick.AutoSize = true;
            checkBoxLoadPictureOnSingleClick.Checked = true;
            checkBoxLoadPictureOnSingleClick.CheckState = CheckState.Checked;
            checkBoxLoadPictureOnSingleClick.Location = new Point(497, 685);
            checkBoxLoadPictureOnSingleClick.Name = "checkBoxLoadPictureOnSingleClick";
            checkBoxLoadPictureOnSingleClick.Size = new Size(231, 19);
            checkBoxLoadPictureOnSingleClick.TabIndex = 24;
            checkBoxLoadPictureOnSingleClick.Text = "Load pictures on single click / keypress";
            checkBoxLoadPictureOnSingleClick.UseVisualStyleBackColor = true;
            // 
            // buttonClear1
            // 
            buttonClear1.Location = new Point(125, 12);
            buttonClear1.Name = "buttonClear1";
            buttonClear1.Size = new Size(56, 23);
            buttonClear1.TabIndex = 25;
            buttonClear1.Text = "Clear";
            buttonClear1.UseVisualStyleBackColor = true;
            buttonClear1.Click += buttonClear1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(403, 12);
            button1.Name = "button1";
            button1.Size = new Size(56, 23);
            button1.TabIndex = 26;
            button1.Text = "Clear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonClear2_Click;
            // 
            // FileGrid
            // 
            FileGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            FileGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FileGrid.Columns.AddRange(new DataGridViewColumn[] { ColumnFileName1, ColumnThumbnail1, ColumnFileName2 });
            FileGrid.Location = new Point(12, 44);
            FileGrid.Name = "FileGrid";
            FileGrid.RowHeadersVisible = false;
            FileGrid.RowTemplate.Height = 50;
            FileGrid.Size = new Size(479, 632);
            FileGrid.TabIndex = 27;
            FileGrid.SelectionChanged += GridSelectionChange;
            FileGrid.KeyDown += FileGrid_KeyDown;
            // 
            // ColumnFileName1
            // 
            ColumnFileName1.HeaderText = "File";
            ColumnFileName1.Name = "ColumnFileName1";
            ColumnFileName1.ReadOnly = true;
            ColumnFileName1.Width = 200;
            // 
            // ColumnThumbnail1
            // 
            ColumnThumbnail1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnThumbnail1.HeaderText = "Thumbnail";
            ColumnThumbnail1.Name = "ColumnThumbnail1";
            // 
            // ColumnFileName2
            // 
            ColumnFileName2.HeaderText = "File";
            ColumnFileName2.Name = "ColumnFileName2";
            ColumnFileName2.ReadOnly = true;
            ColumnFileName2.Width = 200;
            // 
            // Delete1
            // 
            Delete1.HeaderText = "Del";
            Delete1.Name = "Delete1";
            Delete1.Width = 30;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1423, 717);
            Controls.Add(FileGrid);
            Controls.Add(button1);
            Controls.Add(buttonClear1);
            Controls.Add(checkBoxLoadPictureOnSingleClick);
            Controls.Add(labelZoom);
            Controls.Add(buttonZoomMinus);
            Controls.Add(buttonZoomPlus);
            Controls.Add(buttonZoomFit);
            Controls.Add(buttonZoom100);
            Controls.Add(panelForPicture);
            Controls.Add(buttonCompare);
            Controls.Add(buttonDelete1);
            Controls.Add(buttonSelectFolder2);
            Controls.Add(buttonSelectFolder1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "File Match and Delete";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelForPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)FileGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSelectFolder1;
        private Button buttonSelectFolder2;
        private PictureBox pictureBox1;
        private Button buttonDelete1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonCompare;
        private ColumnHeader ColumnFile1;
        private Panel panelForPicture;
        private Button buttonZoom100;
        private Button buttonZoomFit;
        private Button buttonZoomPlus;
        private Button buttonZoomMinus;
        private ColumnHeader ColumnDeleted1;
        private ColumnHeader ColumnMatch1;
        private ColumnHeader ColumnFile2;
        private ColumnHeader ColumnDeleted2;
        private ColumnHeader ColumnMatch2;
        private System.Windows.Forms.Timer timerMarkMatches;
        private Label labelZoom;
        private CheckBox checkBoxLoadPictureOnSingleClick;
        private Button buttonClear1;
        private Button button1;
        private DataGridView FileGrid;
        private DataGridViewCheckBoxColumn Delete1;
        private DataGridViewTextBoxColumn ColumnFileName1;
        private DataGridViewImageColumn ColumnThumbnail1;
        private DataGridViewTextBoxColumn ColumnFileName2;
    }
}