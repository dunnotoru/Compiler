using IDE.Services;
using IDE.Services.Abstractions;
using IDE.View;
using IDE.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace IDE
{
    public partial class App : Application
    {
        private static readonly List<CultureInfo> _languages = new List<CultureInfo>();
        public static event EventHandler? LanguageChanged;
        private readonly IServiceProvider _serviceProvider;
        private NavigationStore _store = new NavigationStore();
        
        public static List<CultureInfo> Languages
        {
            get { return _languages; }
        }
        public static CultureInfo Language
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value is null) throw new ArgumentNullException("null language");
                if (value == Thread.CurrentThread.CurrentUICulture) return;

                Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();

                try
                {
                    dict.Source = new Uri($"Resources/Languages/lang.{value.Name}.xaml", UriKind.Relative);
                }
                catch
                {
                    dict.Source = new Uri($"Resources/Languages/lang.xaml", UriKind.Relative);
                }

                ResourceDictionary? oldDict = (from d in Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.Contains("Resources/Languages/lang.")
                                              select d).FirstOrDefault();

                if(oldDict is not null)
                {
                    int index = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Current.Resources.MergedDictionaries.Remove(oldDict);
                    Current.Resources.MergedDictionaries.Insert(index, dict);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged?.Invoke(Current, EventArgs.Empty);
            }
        }        

        public App()
        {
            LoadResources();
            LoadLanguages();
            _serviceProvider = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Language = new CultureInfo(IDE.Properties.Settings.Default.DefaultLanguage);

            _store.Current = _serviceProvider.GetRequiredService<CodeEnvironmentViewModel>();
            MainWindow = _serviceProvider.GetRequiredService<ShellWindow>();
            MainWindow.DataContext = _serviceProvider.GetRequiredService<ShellViewModel>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void App_LanguageChanged(object? sender, EventArgs e)
        {
            IDE.Properties.Settings.Default.DefaultLanguage = Language.Name;
            IDE.Properties.Settings.Default.Save();
        }

        private void LoadLanguages()
        {
            LanguageChanged += App_LanguageChanged;
            _languages.Clear();
            _languages.Add(new CultureInfo("en_US"));
            _languages.Add(new CultureInfo("ru_RU"));
            _languages.Add(new CultureInfo("zh_CN"));
            _languages.Add(new CultureInfo("tt_RU"));
            _languages.Add(new CultureInfo("de_DE"));
        }

        private void LoadResources()
        {
            ResourceDictionary styles = new ResourceDictionary();
            styles.Source = new Uri("pack://application:,,,/Resources/Styles/Styles.xaml", UriKind.RelativeOrAbsolute);

            ResourceDictionary assets = new ResourceDictionary();
            assets.Source = new Uri("pack://application:,,,/Resources/Assets/Assets.xaml", UriKind.RelativeOrAbsolute);

            Resources.MergedDictionaries.Add(styles);
            Resources.MergedDictionaries.Add(assets);
        }

        private void Close()
        {
            MainWindow.Close();
        }

        private IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>(_store);
            services.AddTransient<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<NavigationService>();

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IMessageBoxService, MessageBoxService>();
            services.AddTransient<IWindowService, WindowService>();
            services.AddTransient<ICloseService>(_ => new CloseService(Close));

            services.AddTransient<IScanService, ScanService>();
            services.AddTransient<IParseService, ParseService>();

            services.AddSingleton(typeof(ILocalizationProvider), new LocalizationProvider(GetLocalizedString));
            services.AddSingleton(typeof(ILogger), ConfigureLogger());

            services.AddSingleton<CodeEnvironmentViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<ShellWindow>();

            return services.BuildServiceProvider();
        }

        private ILogger ConfigureLogger()
        {
            string directory = Environment.CurrentDirectory;
            string logDir = "Log";
            directory = Path.Combine(directory, logDir);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            FileLoggerConfiguration configuration = new FileLoggerConfiguration();
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                builder
                    .AddProvider(new FileLoggerProvider(directory, configuration))
                    .AddFilter("System", LogLevel.Debug)
                    .SetMinimumLevel(LogLevel.Debug));

            return loggerFactory.CreateLogger<FileLogger>();
        }

        private string GetLocalizedString(string key)
        {
            ResourceDictionary dict = (from d in Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.Contains("Resources/Languages/lang.")
                                          select d).First();

            return dict[key] as string ?? string.Empty;
        }
    }
}
