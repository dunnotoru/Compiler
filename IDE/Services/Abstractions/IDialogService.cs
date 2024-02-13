namespace IDE.Services.Abstractions
{
    internal interface IDialogService
    {
        string OpenFileDialog();
        string SaveAsFileDialog();
    }
}
