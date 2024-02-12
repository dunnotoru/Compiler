using IDE.Model.Abstractions;
using System.Windows;

namespace IDE.Model
{
    class MessageBoxService : IMessageBoxService
    {
        public void ShowMessageBox(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }
    }
}
