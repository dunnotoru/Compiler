using IDE.Model;
using IDE.Model.Parser;
using IDE.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace IDE.ViewModel
{
    internal sealed class CodeEnvironmentViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IFileService _fileService;
        private readonly ICloseService _closeService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly ILogger _logger;
        private readonly ILocalizationProvider _localization;
        private readonly IScanService _scanService;
        private readonly IParseService _parseService;
        private readonly ITetradService _tetradService;
        private readonly NavigationService _navigationService;

        private ObservableCollection<TabItemViewModel> _tabs;
        private TabItemViewModel? _selectedTab;

        private ObservableCollection<TokenViewModel> _scanResult;
        private ObservableCollection<ParseErrorViewModel> _parseResult;
        private ObservableCollection<TetradViewModel> _tetrads;
        private ObservableCollection<RegexMatchViewModel> _regexMatchResult;

        public ICommand CreateCommand => new RelayCommand(Create);
        public ICommand SaveCommand => new RelayCommand(Save, _ => SelectedTab != null);
        public ICommand SaveAsCommand => new RelayCommand(SaveAs, _ => SelectedTab != null);
        public ICommand OpenCommand => new RelayCommand(Open);
        public ICommand CloseCommand => new RelayCommand(Close);
        public ICommand NavigateToSettingsCommand => new RelayCommand(NavigateToSettings);
        public ICommand RunCommand => new RelayCommand(Run);
        public ICommand CleanCommand => new RelayCommand(Clean);
        public ICommand ShowTestExamplesCommand => new RelayCommand(ShowTextExamples);
        public ICommand MatchNumbersCommand => new RelayCommand(MatchNumbers);
        public ICommand MatchWordsCommand => new RelayCommand(MatchWords);
        public ICommand MatchUrlsCommand => new RelayCommand(MatchUrls);

        public CodeEnvironmentViewModel(IDialogService dialogService,
                                    IFileService fileService,
                                    ICloseService closeService,
                                    IMessageBoxService messageBoxService,
                                    ILogger logger,
                                    ILocalizationProvider localization,
                                    NavigationService navigationService,
                                    IScanService scanService,
                                    IParseService parseService,
                                    ITetradService semanticsService)
        {
            _tabs = new ObservableCollection<TabItemViewModel>();
            _scanResult = new ObservableCollection<TokenViewModel>();
            _parseResult = new ObservableCollection<ParseErrorViewModel>();
            _tetrads = new ObservableCollection<TetradViewModel>();
            _regexMatchResult = new ObservableCollection<RegexMatchViewModel>();
            _dialogService = dialogService;
            _fileService = fileService;
            _closeService = closeService;
            _messageBoxService = messageBoxService;
            _logger = logger;
            _localization = localization;
            _navigationService = navigationService;
            _scanService = scanService;
            _parseService = parseService;
            _tetradService = semanticsService;
        }

        private void UpdateMatched(string pattern)
        {
            if (SelectedTab is null)
            {
                return;
            }

            RegexMatchResult.Clear();

            MatchCollection list = Regex.Matches(SelectedTab.Content, pattern);
            foreach (Match match in list)
            {
                RegexMatchResult.Add(new RegexMatchViewModel(match.Value, match.Index));
            }
        }

        private void MatchWords(object? obj)
        {
            UpdateMatched(@"\b(?=(?:.*кофе)+)(?=.{10}\b)\w+\b");
        }

        private void MatchNumbers(object? obj)
        {
            UpdateMatched(@"\b\d+(\.\d+)?\b");
        }

        private void MatchUrls(object? obj)
        {
            UpdateMatched(@"\b(http|https|ftp):\/\/[^\s/$.?#].[^\s]*\b");
        }

        private void ShowTextExamples(object? obj)
        {
            string text = string.Empty;
            try
            {
                using (StreamReader sr = File.OpenText(@"Resources\Docs\test_example.txt"))
                {
                    text = sr.ReadToEnd();
                }
                Create(null);
                Tabs.Last().Content = text;
            }
            catch
            {
                _messageBoxService.ShowMessage("Error while opening test_example.txt");
            }
        }

        private void Clean(object? obj)
        {
            if (SelectedTab is null)
            {
                return;
            }

            SelectedTab.Content = SelectedTab.CleanedContent;
            SelectedTab.CanClean = false;
        }

        private void Open(object? obj)
        {
            string fileName = _dialogService.OpenFileDialog();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            TabItemViewModel? tab = Tabs.FirstOrDefault(_ => _.FileName.ToLower().Equals(fileName.ToLower()));
            if (tab is not null)
            {
                SelectedTab = tab;
                return;
            }

            string content = _fileService.LoadFile(fileName);

            tab = new TabItemViewModel(fileName, content);
            tab.IsUnsaved = false;

            AddTab(tab);
            _logger.LogDebug("File opened " + fileName);
        }

        private void Save(object? obj)
        {
            if (SelectedTab is null) return;

            _fileService.SaveFile(SelectedTab.FileName, SelectedTab.Content);

            SelectedTab.IsUnsaved = false;
            _logger.LogDebug("File saved " + SelectedTab.FileName);
        }

        private void SaveAs(object? obj)
        {
            if (SelectedTab is null) return;
            string fileName = _dialogService.SaveAsFileDialog();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            SelectedTab.FileName = fileName;
            _fileService.SaveFile(SelectedTab.FileName, SelectedTab.Content);
            SelectedTab.IsUnsaved = false;

            _logger.LogDebug("File " + fileName + " saved as " + SelectedTab.FileName);
        }

        private void SaveAll()
        {
            foreach (TabItemViewModel tab in Tabs)
                Save(tab);
        }

        private void Create(object? obj)
        {
            string fileName = _dialogService.SaveAsFileDialog();
            if (string.IsNullOrEmpty(fileName)) return;

            TabItemViewModel tab = new TabItemViewModel(fileName);
            tab.IsUnsaved = true;

            AddTab(tab);
            _logger.LogDebug("File created " + fileName);
        }

        private void AddTab(TabItemViewModel tab)
        {
            tab.Close += RemoveTab;
            Tabs.Add(tab);
        }

        private void RemoveTab(object? sender, EventArgs e)
        {
            TabItemViewModel? tab = sender as TabItemViewModel;
            if (tab is null)
            {
                return;
            }


            MessageResult result = MessageResult.No;
            if (tab.IsUnsaved == true)
            {
                result = _messageBoxService.ShowYesNoCancel(_localization.GetLocalizedString("msg_save_changes"));
            }

            switch (result)
            {
                case MessageResult.Yes:
                    _fileService.SaveFile(tab.FileName, tab.Content);
                    break;

                case MessageResult.Cancel:
                    return;

                default:
                    break;
            }


            tab.Close -= RemoveTab;
            Tabs.Remove(tab);
        }

        private void Close(object? obj)
        {
            _closeService.Close();
        }

        public bool AskClose()
        {
            MessageResult result = MessageResult.No;
            if (Tabs.Any(_ => _.IsUnsaved == true))
                result = _messageBoxService.ShowYesNoCancel(_localization.GetLocalizedString("msg_save_changes"));

            switch (result)
            {
                case MessageResult.Yes:
                    SaveAll();
                    return true;

                case MessageResult.Cancel:
                    return false;

                default:
                    return true;
            }
        }

        private void NavigateToSettings(object? obj)
        {
            _navigationService.Navigate<SettingsViewModel>();
        }
        private void Run(object? obj)
        {
            try
            {
                if (SelectedTab is null) return;

                ScanResult.Clear();
                Tetrads.Clear();
                ParseResult.Clear();

                List<Token> tokens = _scanService.Scan(SelectedTab.Content).ToList();
                foreach (Token token in tokens)
                {
                    ScanResult.Add(new TokenViewModel(token));
                }

                (List<ParseError> errors, SelectedTab.CleanedContent) = _parseService.Parse(SelectedTab.Content);
                SelectedTab.CanClean = true;
                foreach (ParseError error in errors)
                {
                    ParseResult.Add(new ParseErrorViewModel(error));
                }

                List<Token> parenthesis = CheckParenthesis(tokens);
                foreach (Token p in parenthesis)
                {
                    ParseResult.Add(new ParseErrorViewModel(new ParseError(p.StartPos, p.EndPos, "Parentesis doesn't match", "")));
                }
                if (parenthesis.Count > 0)
                {
                    return;
                }

                List<Tetrad> tetradsBuffer = _tetradService.GetTetrads(tokens);
                foreach (Tetrad tetrad in tetradsBuffer)
                {
                    Tetrads.Add(new TetradViewModel(tetrad));
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowMessage("An unknown error occured");
            }
        }

        private List<Token> CheckParenthesis(List<Token> tokens)
        {
            tokens = tokens.Where(_ => _.Type == TokenType.OpenParenthesis || _.Type == TokenType.CloseParenthesis).ToList();
            Stack<Token> brackets = new Stack<Token>();
            List<Token> errors = new List<Token>();
            foreach (Token token in tokens)
            {
                if (token.Type == TokenType.OpenParenthesis)
                {
                    brackets.Push(token);
                }
                else if (token.Type == TokenType.CloseParenthesis)
                {
                    if (brackets.Count > 0)
                    {
                        brackets.Pop();
                    }
                    else
                    {
                        errors.Add(token);
                    }
                }
            }

            errors.AddRange(brackets.ToList());

            return errors.ToList();
        }

        public TabItemViewModel? SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TabItemViewModel> Tabs
        {
            get { return _tabs; }
            set { _tabs = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TokenViewModel> ScanResult
        {
            get { return _scanResult; }
            set { _scanResult = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ParseErrorViewModel> ParseResult
        {
            get { return _parseResult; }
            set { _parseResult = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TetradViewModel> Tetrads
        {
            get { return _tetrads; }
            set { _tetrads = value; OnPropertyChanged(); }
        }

        public ObservableCollection<RegexMatchViewModel> RegexMatchResult
        {
            get { return _regexMatchResult; }
            set { _regexMatchResult = value; OnPropertyChanged(); }
        }
    }
}
