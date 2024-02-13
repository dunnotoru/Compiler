using IDE.Services.Abstractions;
using System.Windows;

namespace IDE.Services
{
    class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }

        public MessageResult ShowYesNoCancel(string message, string caption = "")
        {
            return (MessageResult)MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel);
        }
    }
}
