namespace IDE.Model.Abstractions
{
    internal interface IFileService
    {
        void SaveFile(string path, string content);
        string LoadFile(string path);
    }
}
