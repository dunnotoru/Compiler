using System.Globalization;

namespace IDE.ViewModel
{
    class LanguageMenuItem
    {
        public string Name { get; private set; }
        public CultureInfo LangTag { get; private set; }

        public LanguageMenuItem(string name, CultureInfo langTag)
        {
            Name = name;
            LangTag = langTag;
        }
    }
}
