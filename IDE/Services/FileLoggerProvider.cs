using Microsoft.Extensions.Logging;

namespace IDE.Services
{
    internal class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _directory;
        private FileLoggerConfiguration _configuration;

        public FileLoggerProvider(string directory, FileLoggerConfiguration configuration)
        {
            _directory = directory;
            _configuration = configuration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_directory, () => _configuration);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
