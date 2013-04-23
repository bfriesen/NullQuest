using System;
using System.IO;
using System.Text;

namespace NullQuest.Data
{
    public class FileAccess : IFileAccess
    {
        private static readonly string _path =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "NullQuest",
                "SaveGameData.xml");

        public bool DoesFileExist
        {
            get { return File.Exists(_path); }
        }

        public StreamReader CreateReader()
        {
            return new StreamReader(_path, Encoding.UTF8);
        }

        public StreamWriter CreateWriter()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path));
            return new StreamWriter(_path, false, Encoding.UTF8);
        }
    }
}
