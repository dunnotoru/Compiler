using IDE.Services.Abstractions;
using System;

namespace IDE.Services
{
    internal class LocalizationProvider : ILocalizationProvider
    {
        private readonly Func<string, string> _getLang;

        public LocalizationProvider(Func<string, string> getLang)
        {
            _getLang = getLang;
        }

        public string GetLocalizedString(string key)
        {
            return _getLang(key);
        }
    }
}
