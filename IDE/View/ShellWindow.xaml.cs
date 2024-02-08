using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Windows.Documents;

namespace IDE.View
{
    public partial class ShellWindow : Window
    {

        public ShellWindow()
        {
            InitializeComponent();
        }

        private string CurrentFile { get; set; }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text Files (*.txt)|*.txt";
            if (dialog.ShowDialog() != true) return;
            
            TextRange data = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
            using (FileStream fs = File.Create(dialog.FileName))
            {
                try
                { 
                    data.Save(fs, DataFormats.Text);
                    CurrentFile = fs.Name;
                }
                catch { }
            }
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files (*.txt)|*.txt";
            if (dialog.ShowDialog() != true) return;

            TextRange data = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
            using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open))
            {
                try
                {
                    data.Load(fs, DataFormats.Text);
                    CurrentFile = fs.Name;
                }
                catch { }
            }
            
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (CurrentFile != null)
            {
                TextRange doc = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
                using (FileStream fs = File.Create(CurrentFile))
                {
                    try
                    {
                        doc.Save(fs, DataFormats.Text);
                        CurrentFile = fs.Name;
                    }
                    catch { }
                }
            }
        }
    }
}
