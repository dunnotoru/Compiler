using IDE.Model.Abstractions;
using System.IO;

namespace IDE.Model
{
    internal class FileService : IFileService
    {
        public string LoadFile(string path)
        {
            string content;
            using (TextReader tr = File.OpenText(path))
            {
                content = tr.ReadToEnd();
            }
            return content;
        }

        public void SaveFile(string path, string content)
        {
            using (TextWriter tw = File.CreateText(path))
            {
                tw.Write(content);
            }
        }
    }
}
