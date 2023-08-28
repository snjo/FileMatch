using Microsoft.VisualBasic.Devices;
using System;
using System.Drawing.Imaging;
using System.Numerics;
using System.Windows.Forms;

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

            /*if (listView1.Items.Count > 0 && listView2.Items.Count > 0)
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
            }*/

            foreach (FileListing file in fileList)
            {
                ListViewItem lvi = listView.Items.Add(file.FileName);
                lvi.Tag = file.FullPath;
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
            //PictureResize();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;

            PictureZoomFit();
            UpdatePictureFocusPoint();
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

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            listView2.BackColor = Color.Yellow;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            listView2.BackColor = Color.White;
        }


#region Drag Picture
        Vector2 pictureFocusPoint = new Vector2(0f, 0f);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureDragStart(e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            PictureDragStop();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            PictureDragStop();
        }

        // From https://stackoverflow.com/questions/12603709/winform-move-an-image-inside-a-picturebox
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (dragActive && c != null)
            {
                int maxX = pictureBox1.Size.Width * -1 + panelForPicture.Size.Width;
                int maxY = pictureBox1.Size.Height * -1 + panelForPicture.Size.Height;

                int newposLeft = e.X + c.Left - (int)dragPosition.X;
                int newposTop = e.Y + c.Top - (int)dragPosition.Y;

                if (newposTop > 0)
                {
                    newposTop = 0;
                }
                if (newposLeft > 0)
                {
                    newposLeft = 0;
                }
                if (newposLeft < maxX)
                {
                    newposLeft = maxX;
                }
                if (newposTop < maxY)
                {
                    newposTop = maxY;
                }
                c.Top = newposTop;
                c.Left = newposLeft;
                dragCurrent.X = newposLeft;
                dragCurrent.Y = newposTop;
            }

            UpdatePictureFocusPoint();
        }



        Vector2 dragPosition = new Vector2();
        Vector2 dragCurrent = new Vector2();
        bool dragActive = false;

        private void PictureDragStart(int x, int y)
        {
            dragActive = true;
            dragPosition = new Vector2(x, y);// + dragCurrent;
        }

        private void PictureDragStop()
        {
            dragActive = false;
            //dragPosition = dragCurrent;
        }
        #endregion -------------------------------------------------------------------------------------

        #region Zoom Picture -------------------------------------------------------------------------------------

        float currentZoom = 1f;
        private void PictureZoom(float zoom)
        {

            UpdatePictureFocusPoint();
            Vector2 pictureBoxSize = new Vector2(
                Math.Max((int)(pictureBox1.Image.Width * zoom), panelForPicture.Width),
                Math.Max((int)(pictureBox1.Image.Height * zoom), panelForPicture.Height)
            );
            pictureBox1.Width = (int)pictureBoxSize.X;
            pictureBox1.Height = (int)pictureBoxSize.Y;

            Vector2 pictureBoxHalf = new Vector2(pictureBox1.Width / 2, pictureBox1.Height / 2);
            Vector2 panelSize = new Vector2(panelForPicture.Width, panelForPicture.Height);
            Vector2 panelHalf = panelSize / 2;

            Vector2 pictureBoxLocation = -pictureBoxHalf + panelHalf + (pictureFocusPoint * pictureBoxSize);

            pictureBox1.Left = (int)pictureBoxLocation.X;
            pictureBox1.Top = (int)pictureBoxLocation.Y;


            labelTest.Text = "PBL: " + pictureBoxLocation + "    zoom: " + zoom + " pfp: " + pictureFocusPoint;

            int minLeft = -(pictureBox1.Width - panelForPicture.Width);
            int maxLeft = 0;
            if (pictureBox1.Left < minLeft)
                pictureBox1.Left = minLeft;

            if (pictureBox1.Left > maxLeft)
                pictureBox1.Left = maxLeft;

            int minTop = -(pictureBox1.Height - panelForPicture.Height);
            int maxTop = 0;
            if (pictureBox1.Top < minTop)
                pictureBox1.Top = minTop;

            if (pictureBox1.Top > maxTop)
                pictureBox1.Top = maxTop;

            currentZoom = zoom;
            UpdatePictureFocusPoint();
        }

        private void UpdatePictureFocusPoint()
        {
            Vector2 panelSize = new Vector2(panelForPicture.Width, panelForPicture.Height);
            Vector2 panelHalf = panelSize / 2;
            Vector2 pictureSize = new Vector2(pictureBox1.Width, pictureBox1.Height);
            Vector2 pictureHalf = pictureSize / 2;
            Vector2 picturePos = new Vector2(pictureBox1.Left, pictureBox1.Top);
            Vector2 pictureScale = pictureSize / panelSize;

            Vector2 pictureCenter = picturePos + pictureHalf;

            Vector2 centerOffset = (pictureCenter - panelHalf);

            //Vector2 pictureOffset = pictureHalf + picturePos;

            pictureFocusPoint = centerOffset / pictureSize;

            labelOffsetX.Text = "X: " + pictureFocusPoint.X;
            labelOffsetY.Text = "Y: " + pictureFocusPoint.Y + ", " + centerOffset;

            //Vector2 offset = panelCenter - pictureOffset;
            //pictureFocusPoint = offset;// * currentZoom;
        }

        private void buttonZoom100_Click(object sender, EventArgs e)
        {
            PictureZoom(1f);
        }

        private void buttonZoomFit_Click(object sender, EventArgs e)
        {
            PictureZoomFit();
        }

        private void PictureZoomFit()
        {
            float zoomFitX = (float)panelForPicture.Width / (float)pictureBox1.Image.Width;
            float zoomFitY = (float)panelForPicture.Height / (float)pictureBox1.Image.Height;

            //labelTest.Text = zoomFitX + " " + zoomFitY + " from " + panelForPicture.Width + "/" + pictureBox1.Image.Width;

            pictureBox1.Left = 0;
            pictureBox1.Top = 0;

            PictureZoom(Math.Min(zoomFitX, zoomFitY)); // bugged, sends 0f

            //PictureZoom(0.01f);
        }

        private void buttonZoomPlus_Click(object sender, EventArgs e)
        {
            ZoomPlus();
        }

        private void ZoomPlus()
        {
            if (currentZoom >= 0.25f)
            {
                currentZoom += 0.25f;
            }
            else
            {
                currentZoom += 0.1f;
                if (currentZoom > 0.25f) currentZoom = 0.25f;
            }

            if (currentZoom > 4f) currentZoom = 4f;

            PictureZoom(currentZoom);
        }

        private void buttonZoomMinus_Click(object sender, EventArgs e)
        {
            ZoomMinus();
        }

        private void ZoomMinus()
        {
            if (currentZoom <= 0.25f)
            {
                currentZoom -= 0.1f;
            }
            else
            {
                currentZoom -= 0.25f;
                if (currentZoom < 0.25f) currentZoom = 0.25f;
            }

            if (currentZoom < 0.1f)
            {
                currentZoom = 0.1f;
                PictureZoomFit();
            }
            PictureZoom(currentZoom);
        }
        #endregion -------------------------------------------------------------------------------------

    }
}