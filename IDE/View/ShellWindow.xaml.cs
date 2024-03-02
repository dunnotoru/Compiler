using IDE.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;

namespace IDE.View
{
    public partial class ShellWindow : Window
    {
        private WindowState state;
        public ShellWindow()
        {
            InitializeComponent();
            state = (WindowState)Enum.Parse(typeof(WindowState), Properties.Settings.Default.WindowState);
            WindowState = state;

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2;
            this.Left = (screenWidth - this.Width) / 2;
        }

        protected override void OnClosed(EventArgs e)
        {
            Properties.Settings.Default.WindowState = WindowState.ToString();
            Properties.Settings.Default.Save();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CodeEnvironmentViewModel? vm = ContentControl.Content as CodeEnvironmentViewModel;
            if (vm is null)
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = !vm.AskClose();

            base.OnClosing(e);
        }
    }
}
