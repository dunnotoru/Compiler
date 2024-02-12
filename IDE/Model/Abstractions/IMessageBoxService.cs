using System;

namespace IDE.Model.Abstractions
{
    interface IMessageBoxService
    {
        void ShowMessageBox(string message, string caption = "");
    }
}
