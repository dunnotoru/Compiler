namespace IDE.Model.Abstractions
{
    internal interface IDialogService
    {
        string OpenFileDialog();
        string SaveAsFileDialog(string content);
    }
}
