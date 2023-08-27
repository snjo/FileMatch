using System.Drawing.Imaging;

namespace FileMatch
{
    public partial class Form1 : Form
    {
        List<FileListing> files1 = new List<FileListing>();
        List<FileListing> files2 = new List<FileListing>();
        List<string> fileNamesNoExt = new List<string>();
        int scrollPosition = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelectFolder1_Click(object sender, EventArgs e)
        {
            SelectFolder(listView1, files1);
        }
        private void buttonSelectFolder2_Click(object sender, EventArgs e)
        {
            SelectFolder(listView2, files2);
        }

        private void SelectFolder(ListView lv, List<FileListing> fileList)
        {
            DialogResult folderResult = folderBrowserDialog1.ShowDialog();
            if (folderResult == DialogResult.OK)
            {
                LoadFolder(folderBrowserDialog1.SelectedPath, lv, fileList);
            }
        }

        private void LoadFolder(string path, ListView listView, List<FileListing> fileList)
        {
            listView.Items.Clear();
            fileList.Clear();
            string[] filesInFolder = Directory.GetFiles(path);

            foreach (string file in filesInFolder)
            {
                fileList.Add(new FileListing(file, file));
            }

            if (listView1.Items.Count > 0 && listView2.Items.Count > 0)
            {
                CompareLists();
            }
            else
            {
                foreach (FileListing file in fileList)
                {
                    ListViewItem lvi = listView.Items.Add(file.FileName);
                    lvi.Tag = file.FullPath;
                }
            }
        }

        private void CompareLists()
        {
            fileNamesNoExt.Clear();
            AddItemIfUnique(files1);
            AddItemIfUnique(files2);
            List<FileListing> newList1 = new List<FileListing>();
            List<FileListing> newList2 = new List<FileListing>();

            fileNamesNoExt.Sort();

            foreach (string uniqueName in fileNamesNoExt)
            {
                AddLines(newList1, uniqueName, files1);
                AddLines(newList2, uniqueName, files2);
            }

            listView1.Items.Clear();
            listView2.Items.Clear();

            foreach (FileListing fileListing in newList1)
            {
                ListViewItem lvi = listView1.Items.Add(fileListing.FileName);
                lvi.Tag = fileListing.FullPath;
            }
            foreach (FileListing fileListing in newList2)
            {
                ListViewItem lvi = listView2.Items.Add(fileListing.FileName);
                lvi.Tag = fileListing.FullPath;
            }
        }

        private void AddLines(List<FileListing> newList, string uniqueName, List<FileListing> list)
        {
            FileListing fileListing = null;
            foreach (FileListing file in list)
            {
                if (file.GetFileNameWithoutExtension() == uniqueName)
                {
                    fileListing = file;
                    break;
                }
            }
            if (fileListing != null)
            {
                newList.Add(fileListing);
            }
            else
            {
                newList.Add(new FileListing("...", "..."));
            }
        }

        private void AddItemIfUnique(List<FileListing> lv)
        {
            foreach (FileListing file in lv)
            {
                string fileNameWithoutExtension = file.GetFileNameWithoutExtension();
                if (!fileNamesNoExt.Contains(fileNameWithoutExtension))
                {
                    fileNamesNoExt.Add(fileNameWithoutExtension);
                }
            }
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            CompareLists();
        }



        private void AlignViews(int position)
        {
            if (listView1.Items.Count > 0 && listView2.Items.Count > 0)
            {
                listView1.EnsureVisible(Math.Min(position, listView1.Items.Count - 1));
                listView2.EnsureVisible(Math.Min(position, listView2.Items.Count - 1));
                position++;
            }
        }

        private void buttonScrollToTop_Click(object sender, EventArgs e)
        {
            scrollPosition = 0;
            AlignViews(scrollPosition);
        }

        private void buttonScrollToBottom_Click(object sender, EventArgs e)
        {
            scrollPosition = listView1.Items.Count - 1;
            AlignViews(scrollPosition);
        }

        private void buttonScrollUpMore(object sender, EventArgs e)
        {
            Scroll(-10);
        }

        private void buttonScrollDownMore(object sender, EventArgs e)
        {
            Scroll(10);
        }

        private void buttonScrollUp_Click(object sender, EventArgs e)
        {
            Scroll(-1);
        }

        private void buttonScrollDown_Click(object sender, EventArgs e)
        {
            Scroll(1);
        }

        private void Scroll(int amount)
        {
            scrollPosition += amount;
            if (scrollPosition < 0)
                scrollPosition = 0;
            if (scrollPosition >= listView1.Items.Count)
                scrollPosition = listView1.Items.Count - 1;
            AlignViews(scrollPosition);
        }

        private void buttonDeletFiles1_Click(object sender, EventArgs e)
        {
            DeleteFiles(listView1);
        }
        private void buttonDeletFiles2_Click(object sender, EventArgs e)
        {
            DeleteFiles(listView2);
        }

        private void DeleteFiles(ListView listView)
        {
            DialogResult confirmDelete = MessageBox.Show("Delete files?", "Are you sure?", MessageBoxButtons.OKCancel);
            if (confirmDelete != DialogResult.OK) return;
            foreach (ListViewItem selected in listView.SelectedItems)
            {
                //MessageBox.Show("Init " + selected.Tag.ToString());
                bool deleted = false;
                string errorFiles = string.Empty;
                bool error = false;

                if (selected.Tag != null)
                {
                    string fileName = selected.Tag.ToString();
                    if (!File.Exists(fileName))
                    {
                        MessageBox.Show("Can't locate file " + selected.Tag.ToString());
                        return;
                    }
                    try
                    {
                        //MessageBox.Show("Deleting file " + selected.Tag.ToString());
                        File.Delete(fileName);
                        deleted = true;
                    }
                    catch
                    {
                        errorFiles += fileName + " ";
                        error = true;
                    }
                }

                if (deleted)
                {
                    selected.BackColor = Color.LightPink;
                    selected.ForeColor = Color.DarkRed;
                }

                if (error)
                {
                    MessageBox.Show("Couldn't delete files: " + Environment.NewLine + errorFiles);
                }
            }
        }

        private void DisplayPicture(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView.SelectedItems[0];
                if (selected.Tag != null)
                {
                    string fileName = selected.Tag.ToString();
                    if (File.Exists(fileName))
                    {
                        string extension = Path.GetExtension(fileName).ToLower();
                        List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png", ".ico", ".tiff", ".webp" };
                        if (allowedExtensions.Contains(extension))
                        {
                            pictureBox1.ImageLocation = fileName;
                        }
                        else
                        {
                            MessageBox.Show("Not a recognized image extension");
                        }
                    }
                }

            }
        }

        private void pictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            PictureResize();
        }

        private void PictureResize()
        {
            if (pictureBox1.Image != null)
            {
                if (PictureFits(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height))
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private bool PictureFits(Image image, int containerWidth, int ContainerHeight)
        {
            if (image.Width > containerWidth || image.Height > ContainerHeight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            listView1.BackColor = Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            listView1.BackColor = Color.White;
        }
    }
}