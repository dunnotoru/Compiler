using IDE.Services.Abstractions;
using System.IO;

namespace IDE.Services
{
    internal class FileService : IFileService
    {
        public string LoadFile(string path)
        {
            string content;
            using (StreamReader tr = File.OpenText(path))
            {
                content = tr.ReadToEnd();
            }
            return content;
        }

        public void SaveFile(string path, string content)
        {
            using (StreamWriter tw = File.CreateText(path))
            {
                tw.Write(content);
            }
        }
    }
}
