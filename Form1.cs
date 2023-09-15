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

        int columnFilesLeftIndex = 0;
        int columnThumbnailIndex = 1;
        int columnFilesRightIndex = 2;
        Color CellDefaultBackColor = Color.White;
        Color CellDefaultForeColor = Color.Black;
        Color CellMatchtBackColor = Color.White;
        Color CellMatchtForeColor = Color.Blue;
        Color CellNoMatchtBackColor = Color.White;
        Color CellNoMatchtForeColor = Color.Orange;
        Color CellDeletedBackColor = Color.LightPink;
        Color CellDeletedForeColor = Color.DarkRed;

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
            SelectFolder(columnFilesLeftIndex, files1);
        }
        private void buttonSelectFolder2_Click(object sender, EventArgs e)
        {
            SelectFolder(columnFilesRightIndex, files2);
        }

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
            ClearColumn(columnThumbnailIndex);

            fileList.Clear();

            string[] filesInFolder = Directory.GetFiles(path);
            LoadFiles(filesInFolder, column, fileList);
        }

        private void ClearColumn(int column)
        {
            foreach (DataGridViewRow row in FileGrid.Rows)
            {
                row.Cells[column].Value = null;
                row.Cells[column].Tag = null;
                row.Cells[column].Style.BackColor = CellDefaultBackColor;
                row.Cells[column].Style.ForeColor = CellDefaultForeColor;
            }
        }

        private void LoadFiles(string[] files, int column, List<FileListing> fileList)
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                    fileList.Add(new FileListing(file, file));
            }

            if (FileGrid.Rows.Count < fileList.Count)
            {
                FileGrid.Rows.Add(fileList.Count - FileGrid.Rows.Count);

            }

            for (int i = 0; i < fileList.Count; i++)
            {
                FileGrid.Rows[i].Cells[column].Value = fileList[i].FileName;
                FileGrid.Rows[i].Cells[column].Tag = fileList[i];
            }

            FileGrid.Rows[FileGrid.Rows.Count - 1].Height = 50; //set height on last row, doesn't get the right height based on the setting
        }


        private void buttonClear1_Click(object sender, EventArgs e)
        {
            ClearColumn(columnFilesLeftIndex);
            ClearColumn(columnThumbnailIndex);
            files1.Clear();
        }
        private void buttonClear2_Click(object sender, EventArgs e)
        {
            ClearColumn(columnFilesRightIndex);
            ClearColumn(columnThumbnailIndex);
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
                AddLines(newList1, uniqueName, files1, columnFilesLeftIndex);//AddLines(newList1, uniqueName, files1);
                AddLines(newList2, uniqueName, files2, columnFilesRightIndex);
            }

            ClearColumn(columnFilesLeftIndex);
            ClearColumn(columnFilesRightIndex);
            ClearColumn(columnThumbnailIndex);

            ListView lvx = new ListView();

            ColumnAddItems(columnFilesLeftIndex, newList1, newList2);
            ColumnAddItems(columnFilesRightIndex, newList2, newList1);
        }

        private void ColumnAddItems(int column, List<FileListing> newList, List<FileListing> compareToList)
        {
            for (int i = 0; i < newList.Count; i++)
            {
                Debug.WriteLine("column " + column + ", row " + i + ".   FileGrid Rows: " + FileGrid.Rows.Count);
                if (FileGrid.Rows.Count <= i)
                {
                    FileGrid.Rows.Add();
                }
                DataGridViewRow row = FileGrid.Rows[i];
                DataGridViewCell cell = row.Cells[column];
                cell.Value = newList[i].DisplayName;
                cell.Tag = newList[i];

                if (compareToList.Contains(newList[i]))
                {
                    cell.Style.BackColor = CellMatchtBackColor;
                    cell.Style.ForeColor = CellMatchtForeColor;
                }
                else
                {
                    cell.Style.BackColor = CellNoMatchtBackColor;
                    cell.Style.ForeColor = CellNoMatchtForeColor;
                }
            }
        }

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

        #region Delete Files ----------------------------------------------------------------------

        public int ValidSelectedFilesCount(DataGridView grid)
        {
            int validCells = 0;
            foreach (DataGridViewCell selected in grid.SelectedCells)
            {
                if (selected.Tag != null)
                {
                    string fileName = (((FileListing)selected.Tag).FullPath);
                    if (fileName.Length > 0)
                        validCells++;
                }
            }
            return validCells;
        }

        public List<FileListing> ValidSelectedFiles(DataGridView grid)
        {
            List<FileListing> validFiles = new List<FileListing>();
            foreach (DataGridViewCell selected in grid.SelectedCells)
            {
                if (selected.Tag != null)
                {
                    validFiles.Add((FileListing)selected.Tag);
                }
            }
            return validFiles;
        }

        private void DeleteFiles(DataGridView grid) // change to checkbox selection
        {
            if (grid.SelectedCells.Count == 0)
            {
                MessageBox.Show("No files selected", "Delete files");
                return;
            }

            //check if any of the selected cells contain an actual file
            int validCells = ValidSelectedFilesCount(grid);
            if (validCells == 0) return;

            DialogResult confirmDelete = MessageBox.Show("Delete " + validCells + " files?", "Delete files", MessageBoxButtons.OKCancel);
            if (confirmDelete != DialogResult.OK) return;

            string errorFiles = string.Empty;
            bool error = false;

            foreach (DataGridViewCell selected in grid.SelectedCells)
            {
                if (selected.ColumnIndex == columnFilesLeftIndex || selected.ColumnIndex == columnFilesRightIndex)
                {
                    bool deleted = false;

                    if (selected.Tag != null)
                    {
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
                        selected.Style.BackColor = CellDeletedBackColor;
                        selected.Style.ForeColor = CellDeletedForeColor;
                    }
                }
            }
            if (error)
            {
                MessageBox.Show("Couldn't delete files: " + Environment.NewLine + errorFiles + "\nClosing the open files. Try again");
                bmp.Dispose();
                pictureBox1.Image = null;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteFiles(FileGrid);
        }

        private void FileGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteFiles(FileGrid);
            }
        }
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
        private const int exifOrientationID = 0x112; //274
        private void DisplayPicture(object sender, EventArgs e)
        {
            DataGridView? grid = sender as DataGridView;
            if (grid == null) return;
            if (grid.SelectedCells.Count > 0)
            {
                DataGridViewCell selected = grid.SelectedCells[0];
                if (selected.ColumnIndex == columnFilesLeftIndex || selected.ColumnIndex == columnFilesRightIndex)
                {
                    if (selected.Tag != null)
                    {
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
                                bmp = (Bitmap)FromFile(fileName);

                                RotateImageFromExifData(bmp);

                                pictureBox1.Image = bmp;
                                //bmp.Dispose();
                                PictureLoadComplete(sender);
                            }
                            else
                            {
                                pictureBox1.Image = pictureBox1.ErrorImage;
                                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                            }
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

        public static Image FromFile(string path)
        {
            //https://stackoverflow.com/questions/4803935/free-file-locked-by-new-bitmapfilepath

            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            return img;
        }

        private void RotateImageFromExifData(Bitmap image)
        {
            // Image rotation based on EXIF data https://stackoverflow.com/questions/27835064/get-image-orientation-and-rotate-as-per-orientation
            if (!image.PropertyIdList.Contains(exifOrientationID)) return;

            PropertyItem? prop = image.GetPropertyItem(exifOrientationID);

            int val = BitConverter.ToUInt16(prop.Value, 0);
            RotateFlipType rot = RotateFlipType.RotateNoneFlipNone;

            if (val == 3 || val == 4)
                rot = RotateFlipType.Rotate180FlipNone;
            else if (val == 5 || val == 6)
                rot = RotateFlipType.Rotate90FlipNone;
            else if (val == 7 || val == 8)
                rot = RotateFlipType.Rotate270FlipNone;

            if (val == 2 || val == 4 || val == 5 || val == 7)
                rot |= RotateFlipType.RotateNoneFlipX;

            if (rot != RotateFlipType.RotateNoneFlipNone)
            {
                image.RotateFlip(rot);
                image.RemovePropertyItem(exifOrientationID);
            }

        }

        private void pictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //PictureLoadComplete(sender);
        }

        private void PictureLoadComplete(object sender)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            PictureZoomFit();
            UpdatePictureFocusPoint();
            SetThumbnail(((DataGridView)sender).SelectedCells[0]);
        }

        private Vector2 thumbSize = new Vector2(50, 50);
        private void SetThumbnail(DataGridViewCell cell)
        {
            ((DataGridViewImageCell)cell.OwningRow.Cells[columnThumbnailIndex]).Value = CreateThumbnail(bmp, thumbSize);
        }


        private Bitmap CreateThumbnail(Bitmap sourceBitmap, Vector2 size)
        {
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Vector2 ratio = new Vector2(1f, 1f);
            if (size.X > size.Y)
            {
                ratio = new(1f, (float)sourceBitmap.Height / (float)sourceBitmap.Width);
            }
            else
            {
                ratio = new((float)sourceBitmap.Width / (float)sourceBitmap.Height, 1f);
            }
            size = size * ratio;
            Bitmap thumb = new Bitmap((int)size.X, (int)size.Y);
            thumb = (Bitmap)sourceBitmap.GetThumbnailImage((int)size.X, (int)size.Y, myCallback, IntPtr.Zero);
            return thumb;
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private void PictureResize()
        {
            if (pictureBox1.Image == null)
            {
                Debug.WriteLine("PictureResize: Image is null");
                return;
            }

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

        #region Open File Externally -----------------------------------------------------------------
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileExternally();
        }

        private void OpenFileExternally()
        {
            List<FileListing> files = ValidSelectedFiles(FileGrid);
            if (files.Count == 0) return;
            if (files[0] != null)
            {
                new Process { StartInfo = new ProcessStartInfo(files[0].FullPath) { UseShellExecute = true } }.Start();
            }
        }

        private void FileGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileExternally();
        }
        #endregion

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
            dragPosition = new Vector2(x, y);
        }

        private void PictureDragStop()
        {
            dragActive = false;
        }
        #endregion -------------------------------------------------------------------------------------

        #region Zoom Picture ----------------------------------------------------------------------

        float currentZoom = 1f;
        private void PictureZoom(float zoom, bool useMousePosition = false)
        {
            if (pictureBox1.Image == null)
            {
                Debug.WriteLine("PictureZoom: Image is null");
                return;
            }

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
            Point mousePos = panelForPicture.PointToClient(Cursor.Position);
            Vector2 mouseOffset = new Vector2(mousePos.X, mousePos.Y) - panelHalf;
            if (!useMousePosition) mouseOffset = Vector2.Zero;

            Vector2 pictureBoxLocation = -pictureBoxHalf + panelHalf + (pictureFocusPoint * pictureBoxSize) - mouseOffset;

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
            if (pictureBox1.Image == null)
            {
                Debug.WriteLine("PictureZoomFit: Image is null");
                return;
            }
            //Debug.WriteLine("image w:" + pictureBox1.Image.);


            float zoomFitX = (float)panelForPicture.Width / (float)pictureBox1.Image.Width;
            float zoomFitY = (float)panelForPicture.Height / (float)pictureBox1.Image.Height;

            pictureBox1.Left = 0;
            pictureBox1.Top = 0;

            PictureZoom(Math.Min(zoomFitX, zoomFitY));
        }

        private void ZoomPlus(bool useMousePosition = false)
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

            PictureZoom(currentZoom, useMousePosition);
        }

        private void ZoomMinus(bool useMousePosition = false)
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
            PictureZoom(currentZoom, useMousePosition);
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

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomPlus(true);
            }
            if (e.Delta < 0)
            {
                ZoomMinus(true);
            }
        }
        #endregion -------------------------------------------------------------------------------------

        #region File Match ------------------------------------------------------------------------

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
        }


        private static void ClearSubItem(ListView lv1, int num)
        {

        }
        #endregion -------------------------------------------------------------------------


    }
}