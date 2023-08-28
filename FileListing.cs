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
        private string _displayName = string.Empty;
        public string FileNameWithoutExtension = string.Empty;
        public Bitmap Thumbnail;
        public Bitmap Picture;
        public bool Empty = true;
        public bool Deleted = false;

        public FileListing() { }

        public FileListing(bool empty, string displayName = "")
        {
            Empty = empty;
            DisplayName = displayName;
        }

        public FileListing(string filePath, string displayName)
        {
            Folder = Path.GetDirectoryName(filePath);
            FileName = Path.GetFileName(filePath);
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
            Empty = false;
        }

        public FileListing(string folder, string fileName, string displayName)
        {
            Folder = folder;
            FileName = fileName;
            DisplayName = displayName;
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
            Empty = false;
        }

        public string DisplayName
        {
            get 
            {
                if (_displayName == string.Empty)
                    _displayName = FileName;
                
                return _displayName;
            }
            set
            { 
                _displayName = value; 
            }
        }

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
                return Folder + "\\" + FileName;
            }            
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
