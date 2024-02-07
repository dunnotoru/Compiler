using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace IDE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            App.LanguageChanged += App_LanguageChanged;

            CultureInfo currentLanguage = App.Language;

            lang.Items.Clear();
            foreach (var item in App.Languages)
            {
                MenuItem langItem = new MenuItem();
                langItem.Header = item.DisplayName;
                langItem.Tag = item;
                langItem.IsChecked = item.Equals(currentLanguage);
                langItem.Click += LangItem_Click;
                lang.Items.Add(langItem);
            }
        }

        private void LangItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null) return;

            CultureInfo lang = mi.Tag as CultureInfo;
            if (lang == null) return;

            App.Language = lang;
        }

        private void App_LanguageChanged(object? sender, System.EventArgs e)
        {
            CultureInfo currentLanguage = App.Language;

            foreach(MenuItem item in lang.Items)
            {
                CultureInfo ci = item.Tag as CultureInfo;
                item.IsChecked = ci != null && ci.Equals(currentLanguage);
            }
        }
    }
}
