namespace IDE.Model.Abstractions
{
    internal interface IDialogService
    {
        string FilePath { get; }
        bool OpenFileDialog();
        bool SaveFileDialog();
    }
}
