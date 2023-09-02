using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;

namespace FileMatch
{
    public partial class Form1 : Form
    {

        int columnFilesLeft = 1;
        int columnFilesRight = 4;
        public Form1()
        {
            InitializeComponent();
        }

        #region Load folder files + drag and drop -------------------------------------------------
        List<FileListing> files1 = new List<FileListing>();
        List<FileListing> files2 = new List<FileListing>();
        List<string> fileNamesNoExt = new List<string>();
        private void buttonSelectFolder1_Click(object sender, EventArgs e)
        {
            //SelectFolder(listView1, files1);
            SelectFolder(columnFilesLeft, files1);
        }
        private void buttonSelectFolder2_Click(object sender, EventArgs e)
        {
            //SelectFolder(listView2, files2);
            SelectFolder(columnFilesRight, files2);
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
            LoadFiles(filesInFolder, listView, fileList);
        }


        private void LoadFiles(string[] files, ListView listView, List<FileListing> fileList)
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                    fileList.Add(new FileListing(file, file));
            }

            foreach (FileListing file in fileList)
            {
                ListViewItem lvi = listView.Items.Add(file.FileName);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                //lvi.Tag = file.FullPath;
                lvi.Tag = file;
            }
        }

        //---------------------- new test 
        private void SelectFolder(int column, List<FileListing> fileList)
        {
            DialogResult folderResult = folderBrowserDialog1.ShowDialog();
            if (folderResult == DialogResult.OK)
            {
                LoadFolder(folderBrowserDialog1.SelectedPath, column, fileList);
            }
        }
        private void LoadFolder(string path, int column, List<FileListing> fileList)
        {
            ClearColumn(column);

            fileList.Clear();

            string[] filesInFolder = Directory.GetFiles(path);
            LoadFiles(filesInFolder, column, fileList);
        }

        private void ClearColumn(int column)
        {
            foreach (DataGridViewRow row in FileGrid.Rows)
            {
                row.Cells[column].Value = null;
            }
        }

        private void LoadFiles(string[] files, int column, List<FileListing> fileList)
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                    fileList.Add(new FileListing(file, file));
            }

            if (FileGrid.Rows.Count < fileList.Count) FileGrid.Rows.Add(fileList.Count - FileGrid.Rows.Count);

            for (int i = 0; i < fileList.Count; i++)
            {
                FileGrid.Rows[i].Cells[column].Value = fileList[i].FileName;
                FileGrid.Rows[i].Cells[column].Tag = fileList[i];
            }
        }
        // end new test -----------------------------

        /*private void DragDropFileOrFolder(object sender, DragEventArgs e, List<FileListing> targetFilesList)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] dropText = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (dropText != null && dropText.Length > 0)
                {
                    if (Directory.Exists(dropText[0]))
                    {
                        LoadFolder(dropText[0], (ListView)sender, targetFilesList);
                    }
                    else if (File.Exists(dropText[0])) { }
                    {
                        LoadFiles(dropText, (ListView)sender, targetFilesList);
                    }
                }
            }
        }*/
        /*
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            List<FileListing> targetFilesList = files1;
            DragDropFileOrFolder(sender, e, targetFilesList);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects d = DragDropEffects.Copy;
            e.Effect = d;
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            List<FileListing> targetFilesList = files2;
            DragDropFileOrFolder(sender, e, targetFilesList);
        }

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects d = DragDropEffects.Copy;
            e.Effect = d;
        }*/

        private void buttonClear1_Click(object sender, EventArgs e)
        {
            //listView1.Items.Clear();
            ClearColumn(columnFilesLeft);
            files1.Clear();
        }
        private void buttonClear2_Click(object sender, EventArgs e)
        {
            //listView2.Items.Clear();
            ClearColumn(columnFilesRight);
            files2.Clear();
        }
        #endregion ---------------------------------------------------------------------------------------

        #region Compare Lists ---------------------------------------------------------------------
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
                AddLines(newList1, uniqueName, files1, columnFilesLeft);//AddLines(newList1, uniqueName, files1);
                AddLines(newList2, uniqueName, files2, columnFilesRight);
            }

            ClearColumn(columnFilesLeft);
            ClearColumn(columnFilesRight);

            ListView lvx = new ListView();

            ColumnAddItems(columnFilesLeft, newList1, newList2);
            ColumnAddItems(columnFilesRight, newList2, newList1);
        }

        private void ColumnAddItems(int column, List<FileListing> newList, List<FileListing> compareToList)
        {
            //foreach (FileListing fileListing in newList)
            for (int i = 0; i < newList.Count; i++)
            {
                Debug.WriteLine("column " + column + ", row " + i + ".   FileGrid Rows: " + FileGrid.Rows.Count);
                DataGridViewRow row = FileGrid.Rows[i];
                DataGridViewCell cell = row.Cells[column];
                cell.Value = newList[i].DisplayName;
                cell.Tag = newList[i];

                if (compareToList.Contains(newList[i]))
                {
                    cell.Style.ForeColor = Color.DarkBlue;
                }
                else
                {
                    cell.Style.ForeColor = Color.Orange;
                }
            }
        }

        /*private static void ListViewAddItems(List<FileListing> newList, ListView listView, List<FileListing> compareToList)
        {
            foreach (FileListing fileListing in newList)
            {
                ListViewItem lvi = listView.Items.Add(fileListing.DisplayName);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.Tag = fileListing;//.FullPath;
                if (compareToList.Contains(fileListing))
                {
                    lvi.ForeColor = Color.DarkBlue;
                }
                else
                {
                    lvi.ForeColor = Color.Orange;
                }
            }
        }*/

        private void AddLines(List<FileListing> newList, string uniqueName, List<FileListing> list, int column = 0)
        {
            FileListing fileListing = null;
            foreach (FileListing file in list)
            {
                if (file.FileNameWithoutExtension == uniqueName)
                {
                    fileListing = file;
                    break;
                }
            }
            if (fileListing != null)
            {
                newList.Add(fileListing);
                Debug.WriteLine("adding to column " + column + ": " + uniqueName);
            }
            else
            {
                newList.Add(new FileListing(true, "..."));
                Debug.WriteLine("skipping column  " + column + ": " + uniqueName);
            }
        }

        private void AddItemIfUnique(List<FileListing> lv)
        {
            foreach (FileListing file in lv)
            {
                string fileNameWithoutExtension = file.FileNameWithoutExtension;
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
        #endregion --------------------------------------------------------------------------------------

        #region Scroll and align ------------------------------------------------------------------
        //int scrollPosition = 0;
        /*private void AlignViews(int position)
        {
            if (listView1.Items.Count > 0 && listView2.Items.Count > 0)
            {
                listView1.EnsureVisible(Math.Min(position, listView1.Items.Count - 1));
                listView2.EnsureVisible(Math.Min(position, listView2.Items.Count - 1));
                position++;
            }
        }*/
        /*
        private void buttonScrollToTop_Click(object sender, EventArgs e)
        {
            scrollPosition = 0;
            AlignViews(scrollPosition);
        }

        private void buttonScrollToBottom_Click(object sender, EventArgs e)
        {
            scrollPosition = listView1.Items.Count - 1;
            AlignViews(scrollPosition);
        }*/
        /*
        private void buttonScrollUpMore(object sender, EventArgs e)
        {
            ScrollList(-10);
        }

        private void buttonScrollDownMore(object sender, EventArgs e)
        {
            ScrollList(10);
        }

        private void buttonScrollUp_Click(object sender, EventArgs e)
        {
            ScrollList(-1);
        }

        private void buttonScrollDown_Click(object sender, EventArgs e)
        {
            ScrollList(1);
        }*/
        /*
        private void ScrollList(int amount)
        {
            scrollPosition += amount;
            if (scrollPosition < 0)
                scrollPosition = 0;
            if (scrollPosition >= listView1.Items.Count)
                scrollPosition = listView1.Items.Count - 1;
            AlignViews(scrollPosition);
        }*/
        #endregion --------------------------------------------------------------------------------------

        #region Delete Files ----------------------------------------------------------------------
        private void DeleteFiles(DataGridView grid) // change to checkbox selection
        {
            if (grid.SelectedCells.Count == 0)
            {
                MessageBox.Show("No files selected", "Delete files");
                return;
            }

            DialogResult confirmDelete = MessageBox.Show("Delete " + grid.SelectedCells.Count + " files?", "Delete files", MessageBoxButtons.OKCancel);
            if (confirmDelete != DialogResult.OK) return;

            string errorFiles = string.Empty;
            bool error = false;

            foreach (DataGridViewCell selected in grid.SelectedCells)
            {
                if (selected.ColumnIndex == columnFilesLeft || selected.ColumnIndex == columnFilesRight)
                {
                    bool deleted = false;

                    if (selected.Tag != null)
                    {
                        //string fileName = selected.Tag.ToString();
                        string fileName = (selected.Tag as FileListing).FullPath;
                        if (File.Exists(fileName))
                        {
                            try
                            {
                                File.Delete(fileName);
                                deleted = true;
                            }
                            catch
                            {
                                errorFiles += fileName + " ";
                                error = true;
                            }
                        }
                        else
                        {
                            errorFiles += fileName + " ";
                            error = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't delete selected item");
                    }

                    if (deleted)
                    {
                        selected.Style.BackColor = Color.LightPink;
                        selected.Style.ForeColor = Color.DarkRed;
                    }
                }
            }
            if (error)
            {
                MessageBox.Show("Couldn't delete files: " + Environment.NewLine + errorFiles);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteFiles(FileGrid);
        }
        /*
        private void buttonDeletFiles1_Click(object sender, EventArgs e)
        {
            DeleteFiles(listView1);
        }
        private void buttonDeletFiles2_Click(object sender, EventArgs e)
        {
            DeleteFiles(listView2);
        }

        private void buttonDelete1_MouseEnter(object sender, EventArgs e)
        {
            listView1.BackColor = Color.Yellow;
        }

        private void buttonDelete1_MouseLeave(object sender, EventArgs e)
        {
            listView1.BackColor = Color.White;
        }

        private void buttonDelete2_MouseEnter(object sender, EventArgs e)
        {
            listView2.BackColor = Color.Yellow;
        }

        private void buttonDelete2_MouseLeave(object sender, EventArgs e)
        {
            listView2.BackColor = Color.White;
        }*/
        #endregion ------------------------------------------------------------------------

        #region Picture Load and Resize -----------------------------------------------------------

        private void GridSelectionChange(object sender, EventArgs e)
        {
            if (checkBoxLoadPictureOnSingleClick.Checked)
            {
                DisplayPicture(sender, e);
            }
        }

        Bitmap bmp;
        private void DisplayPicture(object sender, EventArgs e)
        {
            DataGridView? grid = sender as DataGridView;
            if (grid == null) return;
            if (grid.SelectedCells.Count > 0)
            {
                DataGridViewCell selected = grid.SelectedCells[0];
                if (selected.ColumnIndex == columnFilesLeft || selected.ColumnIndex == columnFilesRight)
                {
                    if (selected.Tag != null)
                    {
                        //string fileName = selected.Tag.ToString();
                        string fileName = (selected.Tag as FileListing).FullPath;
                        if (File.Exists(fileName))
                        {
                            string extension = Path.GetExtension(fileName).ToLower();
                            List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png", ".ico", ".tiff", ".webp" };
                            if (allowedExtensions.Contains(extension))
                            {
                                //method 1
                                //pictureBox1.ImageLocation = fileName; //works with PictureBox LoadCompleted, but doesn't release memory (Dispose)

                                //method 2
                                if (bmp != null) bmp.Dispose();
                                bmp = new Bitmap(fileName);
                                pictureBox1.Image = bmp;
                                PictureLoadComplete();
                            }
                            else
                            {
                                pictureBox1.Image = pictureBox1.ErrorImage;
                                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                            }
                        }
                    }
                }

            }
        }

        private void pictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            PictureLoadComplete();
        }

        private void PictureLoadComplete()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            //MessageBox.Show(bmp.Width + ", " + bmp.Height + " / " + pictureBox1.Image.Width + ", " + pictureBox1.Image.Height);
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

        private void listView_Click(object sender, EventArgs e)
        {
            if (checkBoxLoadPictureOnSingleClick.Checked)
            {
                DisplayPicture(sender, e);
            }
        }

        private void listView_KeyUp(object sender, KeyEventArgs e)
        {
            if (checkBoxLoadPictureOnSingleClick.Checked)
            {
                DisplayPicture(sender, e);
            }
        }

        #endregion -----------------------------------------------------------------------------------------------

        #region Drag Picture ----------------------------------------------------------------------
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

        #region Zoom Picture ----------------------------------------------------------------------

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


            labelZoom.Text = "Zoom: " + Math.Round(zoom * 100f) + "%";

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

            pictureFocusPoint = centerOffset / pictureSize;

        }

        private void PictureZoomFit()
        {
            float zoomFitX = (float)panelForPicture.Width / (float)pictureBox1.Image.Width;
            float zoomFitY = (float)panelForPicture.Height / (float)pictureBox1.Image.Height;

            pictureBox1.Left = 0;
            pictureBox1.Top = 0;

            PictureZoom(Math.Min(zoomFitX, zoomFitY)); // bugged, sends 0f

            //PictureZoom(0.01f);
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

        private void buttonZoomPlus_Click(object sender, EventArgs e)
        {
            ZoomPlus();
        }

        private void buttonZoomMinus_Click(object sender, EventArgs e)
        {
            ZoomMinus();
        }

        private void buttonZoom100_Click(object sender, EventArgs e)
        {
            PictureZoom(1f);
        }

        private void buttonZoomFit_Click(object sender, EventArgs e)
        {
            PictureZoomFit();
        }
        #endregion -------------------------------------------------------------------------------------

        #region File Match ------------------------------------------------------------------------
        /*
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //SelectItemHighlightMatches(listView1, listView2);
            StartMatchingTimer(listView1, listView2);
        }

        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            StartMatchingTimer(listView2, listView1);
        }*/

        private void StartMatchingTimer(ListView lv1, ListView lv2)
        {
            if (!markMatchStarted)
            {
                markMatchStarted = true;
                timerMarkMatches.Start();
                matchList1 = lv1;
                matchList2 = lv2;
            }
        }

        private bool markMatchStarted = false;
        private ListView matchList1;
        private ListView matchList2;

        private void timer1_Tick(object sender, EventArgs e)
        {
            markMatchStarted = false;
            timerMarkMatches.Stop();
            //SelectItemHighlightMatches(matchList1, matchList2);
        }

        /*
        private void SelectItemHighlightMatches(ListView lv1, ListView lv2)
        {
            if (!checkBoxMarkMatches.Checked) return;
            if (lv1.SelectedItems.Count > 0)
            {
                ClearSubItem(lv1, 2);
                ClearSubItem(lv2, 2);
                int selectNumber = 1;

                foreach (ListViewItem item1 in lv1.SelectedItems)
                {
                    if (item1.SubItems.Count < 2) item1.SubItems.Add("");
                    if (item1.SubItems.Count < 3) item1.SubItems.Add("");
                    //string name1 = Path.GetFileNameWithoutExtension(item1.Tag.ToString());
                    string name1 = (item1.Tag as FileListing).FileNameWithoutExtension;
                    bool found = false;


                    foreach (ListViewItem item2 in lv2.Items)
                    {
                        //if (item1.Text != "...")
                        if (!(item1.Tag as FileListing).Empty)
                        {
                            string name2 = (item2.Tag as FileListing).FileNameWithoutExtension;
                            if (name1 == name2)
                            {
                                item1.SubItems[2].Text = "=" + selectNumber + "="; //"\u2B9C";
                                item2.SubItems[2].Text = "=" + selectNumber + "="; //"\u2B05";
                                selectNumber++;
                                found = true;
                                break;
                            }

                        }
                    }
                    if (!found)
                    {
                        item1.SubItems[2].Text = "X";
                    }

                }
            }
        }*/

        private static void ClearSubItem(ListView lv1, int num)
        {
            foreach (ListViewItem lvi in lv1.Items) // clear old markings
            {
                if (lvi.SubItems.Count > num)
                    lvi.SubItems[num].Text = "";
            }
        }
        /*
        private void checkBoxMarkMatches_CheckedChanged(object sender, EventArgs e)
        {
            ClearSubItem(listView1 as ListView, 1);
            ClearSubItem(listView1 as ListView, 2);
            ClearSubItem(listView2 as ListView, 1);
            ClearSubItem(listView2 as ListView, 2);
        }*/
        #endregion -------------------------------------------------------------------------


    }
}