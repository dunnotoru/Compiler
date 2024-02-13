using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace IDE.Services
{
    internal sealed class FileLogger : ILogger
    {
        private readonly string _directory;
        Func<FileLoggerConfiguration> _getCurrentConfig;

        public FileLogger(string directory, Func<FileLoggerConfiguration> getCurrentConfig)
        {
            _directory = directory;
            _getCurrentConfig = getCurrentConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel)
            => _getCurrentConfig().LogLevelToStringMap.ContainsKey(logLevel);
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            FileLoggerConfiguration configuration = _getCurrentConfig();

            StringBuilder sb = new StringBuilder();
        }
    }
}
