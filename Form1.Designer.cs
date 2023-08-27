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
            buttonSelectFolder1 = new Button();
            listView1 = new ListView();
            ColumnHeader1 = new ColumnHeader();
            listView2 = new ListView();
            columnHeader2 = new ColumnHeader();
            buttonSelectFolder2 = new Button();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonCompare = new Button();
            buttonScrollToTop = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            listView1.Columns.AddRange(new ColumnHeader[] { ColumnHeader1 });
            listView1.Location = new Point(12, 41);
            listView1.Name = "listView1";
            listView1.Size = new Size(265, 614);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.DoubleClick += DisplayPicture;
            // 
            // ColumnHeader1
            // 
            ColumnHeader1.Text = "File Name";
            ColumnHeader1.Width = 250;
            // 
            // listView2
            // 
            listView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader2 });
            listView2.Location = new Point(319, 41);
            listView2.Name = "listView2";
            listView2.Size = new Size(265, 614);
            listView2.TabIndex = 3;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            listView2.DoubleClick += DisplayPicture;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "File Name";
            columnHeader2.Width = 250;
            // 
            // buttonSelectFolder2
            // 
            buttonSelectFolder2.Location = new Point(283, 12);
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
            pictureBox1.Location = new Point(590, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(774, 614);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.LoadCompleted += pictureBox1_LoadCompleted;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(170, 661);
            button1.Name = "button1";
            button1.Size = new Size(107, 23);
            button1.TabIndex = 5;
            button1.Text = "Delete Selected";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonDeletFiles1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
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
            // button2
            // 
            button2.Location = new Point(477, 661);
            button2.Name = "button2";
            button2.Size = new Size(107, 23);
            button2.TabIndex = 13;
            button2.Text = "Delete Selected";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonDeletFiles2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 696);
            Controls.Add(button2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(buttonScrollToTop);
            Controls.Add(buttonCompare);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(listView2);
            Controls.Add(buttonSelectFolder2);
            Controls.Add(listView1);
            Controls.Add(buttonSelectFolder1);
            Name = "Form1";
            Text = "File Match and Delete";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonSelectFolder1;
        private ListView listView1;
        private ListView listView2;
        private Button buttonSelectFolder2;
        private PictureBox pictureBox1;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonCompare;
        private ColumnHeader ColumnHeader1;
        private ColumnHeader columnHeader2;
        private Button buttonScrollToTop;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button2;
    }
}