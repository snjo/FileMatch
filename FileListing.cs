using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMatch
{
    public class FileListing
    {
        public string Folder = string.Empty;
        public string FileName = string.Empty;
        public string DisplayName = string.Empty;
        public Bitmap Thumbnail;
        public Bitmap Picture;
        public bool Empty = true;
        public bool Deleted = false;

        public FileListing() { }

        public FileListing(bool empty)
        {
            Empty = empty;
        }

        public FileListing(string filePath, string displayName)
        {
            Folder = Path.GetDirectoryName(filePath);
            FileName = Path.GetFileName(filePath);
            //SetDisplayName(displayName);
            Empty = false;
        }

        public FileListing(string folder, string fileName, string displayName)
        {
            Folder = folder;
            FileName = fileName;
            DisplayName = displayName;
            //SetDisplayName(displayName);
            Empty = false;
        }

        /*private void SetDisplayName(string displayName)
        {
            if (displayName == "")
            {
                DisplayName = FileName;
            }
            else
            {
                DisplayName = displayName;
            }
        }*/

        public string Extension
        {
            get
            {
                return Path.GetExtension(FileName);
            }
        }

        public string FullPath
        {
            get
            {
                //string folder = Path.GetDirectoryName(Folder); // removes trailing backslashes
                //return folder + "\\" + FileName;
                return Folder + "\\" + FileName;
            }            
        }

        public string GetFileNameWithoutExtension()
        {
            return Path.GetFileNameWithoutExtension(FileName);
        }

        public bool FileExists()
        {
            return File.Exists(FullPath);
        }

        public bool FolderExists()
        {
            return Directory.Exists(Folder);
        }
    }
}
