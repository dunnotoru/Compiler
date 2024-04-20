using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace IDE.Services
{
    internal sealed class FileLogger : ILogger
    {
        private readonly string _path;
        Func<FileLoggerConfiguration> _getCurrentConfig;
        private object _lock = new object();

        public FileLogger(string directory, Func<FileLoggerConfiguration> getCurrentConfig)
        {
            _getCurrentConfig = getCurrentConfig;

            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt";
            _path = Path.Combine(directory, fileName);
            File.Create(_path);
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel)
            => _getCurrentConfig().LogLevelToStringMap.ContainsKey(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            FileLoggerConfiguration configuration = _getCurrentConfig();

            lock (_lock)
            {
                FileStream fs = File.Open(_path, FileMode.Append);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"[{configuration.LogLevelToStringMap[logLevel]}] " + formatter(state, exception));
                }
                fs.Dispose();
            }
        }
    }
}
