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
            buttonSelectFolder1 = new Button();
            listView1 = new ListView();
            ColumnFile1 = new ColumnHeader();
            ColumnDeleted1 = new ColumnHeader();
            ColumnMatch1 = new ColumnHeader();
            buttonSelectFolder2 = new Button();
            pictureBox1 = new PictureBox();
            buttonDelete1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonCompare = new Button();
            buttonScrollToTop = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            buttonDelete2 = new Button();
            panelForPicture = new Panel();
            buttonZoom100 = new Button();
            buttonZoomFit = new Button();
            buttonZoomPlus = new Button();
            buttonZoomMinus = new Button();
            listView2 = new ListView();
            ColumnFile2 = new ColumnHeader();
            ColumnDeleted2 = new ColumnHeader();
            ColumnMatch2 = new ColumnHeader();
            checkBoxMarkMatches = new CheckBox();
            timerMarkMatches = new System.Windows.Forms.Timer(components);
            labelZoom = new Label();
            checkBoxLoadPictureOnSingleClick = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelForPicture.SuspendLayout();
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
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listView1.Columns.AddRange(new ColumnHeader[] { ColumnFile1, ColumnDeleted1, ColumnMatch1 });
            listView1.Location = new Point(12, 41);
            listView1.Name = "listView1";
            listView1.Size = new Size(265, 614);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ItemSelectionChanged += listView1_ItemSelectionChanged;
            listView1.Click += listView_Click;
            listView1.DoubleClick += DisplayPicture;
            listView1.KeyUp += listView_KeyUp;
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
            buttonSelectFolder2.Location = new Point(319, 12);
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
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(768, 608);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.LoadCompleted += pictureBox1_LoadCompleted;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // buttonDelete1
            // 
            buttonDelete1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDelete1.Location = new Point(170, 661);
            buttonDelete1.Name = "buttonDelete1";
            buttonDelete1.Size = new Size(107, 23);
            buttonDelete1.TabIndex = 5;
            buttonDelete1.Text = "Delete Selected";
            buttonDelete1.UseVisualStyleBackColor = true;
            buttonDelete1.Click += buttonDeletFiles1_Click;
            buttonDelete1.MouseEnter += buttonDelete1_MouseEnter;
            buttonDelete1.MouseLeave += buttonDelete1_MouseLeave;
            // 
            // buttonCompare
            // 
            buttonCompare.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonCompare.Location = new Point(12, 661);
            buttonCompare.Name = "buttonCompare";
            buttonCompare.Size = new Size(107, 23);
            buttonCompare.TabIndex = 6;
            buttonCompare.Text = "Compare";
            buttonCompare.UseVisualStyleBackColor = true;
            buttonCompare.Click += buttonCompare_Click;
            // 
            // buttonScrollToTop
            // 
            buttonScrollToTop.Location = new Point(283, 153);
            buttonScrollToTop.Name = "buttonScrollToTop";
            buttonScrollToTop.Size = new Size(30, 37);
            buttonScrollToTop.TabIndex = 8;
            buttonScrollToTop.Text = "🡱";
            buttonScrollToTop.UseVisualStyleBackColor = true;
            buttonScrollToTop.Click += buttonScrollUpMore;
            // 
            // button3
            // 
            button3.Location = new Point(283, 196);
            button3.Name = "button3";
            button3.Size = new Size(30, 37);
            button3.TabIndex = 10;
            button3.Text = "⮝";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonScrollUp_Click;
            // 
            // button4
            // 
            button4.Location = new Point(283, 239);
            button4.Name = "button4";
            button4.Size = new Size(30, 37);
            button4.TabIndex = 11;
            button4.Text = "⮟";
            button4.UseVisualStyleBackColor = true;
            button4.Click += buttonScrollDown_Click;
            // 
            // button5
            // 
            button5.Location = new Point(283, 282);
            button5.Name = "button5";
            button5.Size = new Size(30, 37);
            button5.TabIndex = 12;
            button5.Text = "🡳";
            button5.UseVisualStyleBackColor = true;
            button5.Click += buttonScrollDownMore;
            // 
            // buttonDelete2
            // 
            buttonDelete2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDelete2.Location = new Point(477, 661);
            buttonDelete2.Name = "buttonDelete2";
            buttonDelete2.Size = new Size(107, 23);
            buttonDelete2.TabIndex = 13;
            buttonDelete2.Text = "Delete Selected";
            buttonDelete2.UseVisualStyleBackColor = true;
            buttonDelete2.Click += buttonDeletFiles2_Click;
            buttonDelete2.MouseEnter += buttonDelete2_MouseEnter;
            buttonDelete2.MouseLeave += buttonDelete2_MouseLeave;
            // 
            // panelForPicture
            // 
            panelForPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelForPicture.AutoScroll = true;
            panelForPicture.BackColor = Color.Black;
            panelForPicture.Controls.Add(pictureBox1);
            panelForPicture.Location = new Point(590, 41);
            panelForPicture.Name = "panelForPicture";
            panelForPicture.Size = new Size(774, 614);
            panelForPicture.TabIndex = 14;
            // 
            // buttonZoom100
            // 
            buttonZoom100.Location = new Point(593, 12);
            buttonZoom100.Name = "buttonZoom100";
            buttonZoom100.Size = new Size(53, 23);
            buttonZoom100.TabIndex = 15;
            buttonZoom100.Text = "100%";
            buttonZoom100.UseVisualStyleBackColor = true;
            buttonZoom100.Click += buttonZoom100_Click;
            // 
            // buttonZoomFit
            // 
            buttonZoomFit.Location = new Point(652, 12);
            buttonZoomFit.Name = "buttonZoomFit";
            buttonZoomFit.Size = new Size(39, 23);
            buttonZoomFit.TabIndex = 16;
            buttonZoomFit.Text = "Fit";
            buttonZoomFit.UseVisualStyleBackColor = true;
            buttonZoomFit.Click += buttonZoomFit_Click;
            // 
            // buttonZoomPlus
            // 
            buttonZoomPlus.Location = new Point(697, 12);
            buttonZoomPlus.Name = "buttonZoomPlus";
            buttonZoomPlus.Size = new Size(39, 23);
            buttonZoomPlus.TabIndex = 17;
            buttonZoomPlus.Text = "+";
            buttonZoomPlus.UseVisualStyleBackColor = true;
            buttonZoomPlus.Click += buttonZoomPlus_Click;
            // 
            // buttonZoomMinus
            // 
            buttonZoomMinus.Location = new Point(742, 12);
            buttonZoomMinus.Name = "buttonZoomMinus";
            buttonZoomMinus.Size = new Size(39, 23);
            buttonZoomMinus.TabIndex = 18;
            buttonZoomMinus.Text = "-";
            buttonZoomMinus.UseVisualStyleBackColor = true;
            buttonZoomMinus.Click += buttonZoomMinus_Click;
            // 
            // listView2
            // 
            listView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listView2.Columns.AddRange(new ColumnHeader[] { ColumnFile2, ColumnDeleted2, ColumnMatch2 });
            listView2.Location = new Point(319, 44);
            listView2.Name = "listView2";
            listView2.Size = new Size(265, 614);
            listView2.TabIndex = 22;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            listView2.ItemSelectionChanged += listView1_ItemSelectionChanged;
            listView2.Click += listView_Click;
            listView2.DoubleClick += DisplayPicture;
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
            // checkBoxMarkMatches
            // 
            checkBoxMarkMatches.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxMarkMatches.AutoSize = true;
            checkBoxMarkMatches.Checked = true;
            checkBoxMarkMatches.CheckState = CheckState.Checked;
            checkBoxMarkMatches.Location = new Point(319, 664);
            checkBoxMarkMatches.Name = "checkBoxMarkMatches";
            checkBoxMarkMatches.Size = new Size(101, 19);
            checkBoxMarkMatches.TabIndex = 23;
            checkBoxMarkMatches.Text = "Mark matches";
            checkBoxMarkMatches.UseVisualStyleBackColor = true;
            checkBoxMarkMatches.CheckedChanged += checkBoxMarkMatches_CheckedChanged;
            // 
            // timerMarkMatches
            // 
            timerMarkMatches.Tick += timer1_Tick;
            // 
            // labelZoom
            // 
            labelZoom.AutoSize = true;
            labelZoom.Location = new Point(798, 16);
            labelZoom.Name = "labelZoom";
            labelZoom.Size = new Size(39, 15);
            labelZoom.TabIndex = 21;
            labelZoom.Text = "Zoom";
            // 
            // checkBoxLoadPictureOnSingleClick
            // 
            checkBoxLoadPictureOnSingleClick.AutoSize = true;
            checkBoxLoadPictureOnSingleClick.Checked = true;
            checkBoxLoadPictureOnSingleClick.CheckState = CheckState.Checked;
            checkBoxLoadPictureOnSingleClick.Location = new Point(593, 664);
            checkBoxLoadPictureOnSingleClick.Name = "checkBoxLoadPictureOnSingleClick";
            checkBoxLoadPictureOnSingleClick.Size = new Size(231, 19);
            checkBoxLoadPictureOnSingleClick.TabIndex = 24;
            checkBoxLoadPictureOnSingleClick.Text = "Load pictures on single click / keypress";
            checkBoxLoadPictureOnSingleClick.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 696);
            Controls.Add(checkBoxLoadPictureOnSingleClick);
            Controls.Add(checkBoxMarkMatches);
            Controls.Add(listView2);
            Controls.Add(labelZoom);
            Controls.Add(buttonZoomMinus);
            Controls.Add(buttonZoomPlus);
            Controls.Add(buttonZoomFit);
            Controls.Add(buttonZoom100);
            Controls.Add(panelForPicture);
            Controls.Add(buttonDelete2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(buttonScrollToTop);
            Controls.Add(buttonCompare);
            Controls.Add(buttonDelete1);
            Controls.Add(buttonSelectFolder2);
            Controls.Add(listView1);
            Controls.Add(buttonSelectFolder1);
            Name = "Form1";
            Text = "File Match and Delete";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelForPicture.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSelectFolder1;
        private ListView listView1;
        private Button buttonSelectFolder2;
        private PictureBox pictureBox1;
        private Button buttonDelete1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonCompare;
        private ColumnHeader ColumnFile1;
        private Button buttonScrollToTop;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button buttonDelete2;
        private Panel panelForPicture;
        private Button buttonZoom100;
        private Button buttonZoomFit;
        private Button buttonZoomPlus;
        private Button buttonZoomMinus;
        private ColumnHeader ColumnDeleted1;
        private ColumnHeader ColumnMatch1;
        private ListView listView2;
        private ColumnHeader ColumnFile2;
        private ColumnHeader ColumnDeleted2;
        private ColumnHeader ColumnMatch2;
        private CheckBox checkBoxMarkMatches;
        private System.Windows.Forms.Timer timerMarkMatches;
        private Label labelZoom;
        private CheckBox checkBoxLoadPictureOnSingleClick;
    }
}