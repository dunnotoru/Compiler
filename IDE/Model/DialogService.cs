﻿using IDE.Model.Abstractions;
using Microsoft.Win32;

namespace IDE.Model
{
    internal class DialogService : IDialogService
    {
        public string OpenFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.DefaultExt = "txt";
            dialog.ShowDialog();

            return dialog.FileName;
        }

        public string SaveAsFileDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.DefaultExt = "txt";
            dialog.ShowDialog();

            return dialog.FileName;
        }
    }
}
