namespace IDE.Services.Abstractions
{
    internal interface IFileService
    {
        void SaveFile(string path, string content);
        string LoadFile(string path);
    }
}
