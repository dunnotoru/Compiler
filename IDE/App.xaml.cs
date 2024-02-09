using IDE.Model;
using IDE.Model.Abstractions;
using IDE.View;
using IDE.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace IDE
{
    public partial class App : Application
    {
        private static List<CultureInfo> _languages = new List<CultureInfo>();
        public static event EventHandler? LanguageChanged;
        private IServiceProvider _serviceProvider;

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
                if (value == null) throw new ArgumentNullException("null language");
                if (value == Thread.CurrentThread.CurrentUICulture) return;

                Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch(value.Name)
                {
                    case "ru_RU":
                        dict.Source = new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri($"Resources/lang.xaml", UriKind.Relative);
                        break;
                        
                }

                ResourceDictionary oldDict = (from d in Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();

                if(oldDict != null)
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
            _languages.Clear();
            _languages.Add(new CultureInfo("en_US"));


        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ResourceDictionary langResources = new ResourceDictionary();
            langResources.Source = new Uri("Resources/Languages/lang.xaml", UriKind.RelativeOrAbsolute);

            ResourceDictionary styleResourses = new ResourceDictionary();
            langResources.Source = new Uri("Resources/Styles/Styles.xaml", UriKind.RelativeOrAbsolute);
            
            Resources.MergedDictionaries.Add(langResources);
            Resources.MergedDictionaries.Add(styleResourses);

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddSingleton<ShellWindowViewModel>();
            _serviceProvider = services.BuildServiceProvider();

            ShellWindow window = new ShellWindow();
            MainWindow = window;
            MainWindow.DataContext = _serviceProvider.GetService<ShellWindowViewModel>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
