using IDE.Model.Abstractions;
using Microsoft.Win32;

namespace IDE.Model
{
    internal class DialogService : IDialogService
    {
        IFileService _fileService;

        public DialogService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public string OpenFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.DefaultExt = "txt";
            dialog.ShowDialog();

            return dialog.FileName;
        }

        public string SaveAsFileDialog(string content)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.DefaultExt = "txt";
            dialog.ShowDialog();

            _fileService.SaveFile(dialog.FileName, content);

            return dialog.FileName;
        }
    }
}
