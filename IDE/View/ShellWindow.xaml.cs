using System.Windows;

namespace IDE.View
{
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2;
            this.Left = (screenWidth - this.Width) / 2;
        }
    }
}
