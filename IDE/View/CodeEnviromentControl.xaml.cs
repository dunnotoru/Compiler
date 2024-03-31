using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace IDE.View
{
    public partial class CodeEnvironmentControl : UserControl
    {
        public CodeEnvironmentControl()
        {
            InitializeComponent();
        }

        private void Open(string fileName)
        {
            try
            {
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(fileName)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
            catch
            {
                MessageBox.Show($"Error while opening {fileName}");
            }
        }

        private void TaskMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\about.html");
        }

        private void GrammarMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\grammar.html");
        }

        private void AnalyzeMethodMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\analyze_method.html");
        }

        private void ErrorDiagnosisMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\error_diagnosis.html");
        }

        private void ReferenceListMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\reference_list.html");
        }

        private void SourceCodeMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"https://github.com/dunnotoru/Compiler");
        }

        private void HelpMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\help.html");
        }

        private void AboutMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Open(@"Resources\Docs\about.html");
        }
    }
}
