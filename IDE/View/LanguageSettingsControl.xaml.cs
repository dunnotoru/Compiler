using System;
using System.Globalization;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IDE.View
{
    public partial class LanguageSettingsControl : UserControl
    {
        public LanguageSettingsControl()
        {
            InitializeComponent();

            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;

            langs.Items.Clear();
            foreach (CultureInfo lang in App.Languages)
            {
                ComboBoxItem menuLang = new ComboBoxItem();
                menuLang.Content = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.Selected += OnLanguageSelected;
                if (lang.Equals(currLang))
                    langs.SelectedItem = menuLang;
                langs.Items.Add(menuLang);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            foreach (ComboBoxItem i in langs.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                if (ci.Equals(currLang))
                    langs.SelectedItem = i;
            }
        }

        private void OnLanguageSelected(Object sender, EventArgs e)
        {
            ComboBoxItem mi = sender as ComboBoxItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }

        }


    }
}
