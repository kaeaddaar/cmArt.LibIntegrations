using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace cmArt.LibIntegrations.FileNamesService
{
    public class FileNameService
    {
        private string _filePathAndName;
        public FileNameService()
        {
            _filePathAndName = string.Empty;
        }
        public FileNameService(string FilePathAndName)
        {
            _filePathAndName = FilePathAndName ?? string.Empty;
        }
        public void Init(string FilePathAndName)
        {
            _filePathAndName = FilePathAndName ?? string.Empty;
        }
        public string GetFileName()
        {
            int IndexLastSlash = GetPosLastOccurance('\\');
            int LenFilePathAndName = _filePathAndName.Length;
            int LenFileName = LenFilePathAndName - IndexLastSlash;
            string fileName = _filePathAndName.Substring(IndexLastSlash, LenFileName);
            return fileName;
        }
        public string GetPath()
        {
            int LenFileName = _filePathAndName.Length;
            int IndexLastSlash = GetPosLastOccurance('\\');
            int LenExtension = LenFileName - IndexLastSlash;
            string Path = _filePathAndName.Substring(0, IndexLastSlash);
            return Path;
        }
        public string GetExtention()
        {
            if (_filePathAndName == string.Empty)
            { return string.Empty; }

            string backwards = string.Join(string.Empty, _filePathAndName.Reverse());
            bool HasAnExtension = backwards.Contains('.');
            
            if (!HasAnExtension)
            { return string.Empty; }

            int LenFileName = _filePathAndName.Length;
            int IndexLastDot = GetPosLastOccurance('.');
            int LenExtension = LenFileName - IndexLastDot;
            string Extension = _filePathAndName.Substring(IndexLastDot, LenExtension);
            return Extension;
        }
        private int GetPosLastOccurance(char ToCheckFor)
        {
            int NotFound = 0;
            if (_filePathAndName == string.Empty)
            { return 0; }

            string backwards = string.Join(string.Empty, _filePathAndName.Reverse());
            bool CharacterExists = backwards.Contains(ToCheckFor);

            if (!CharacterExists)
            { return NotFound; }

            int PosBackwards = backwards.IndexOf(ToCheckFor);
            int LenFileName = _filePathAndName.Length;
            int PosChar = LenFileName - PosBackwards;

            return PosChar;
        }
    }
}
