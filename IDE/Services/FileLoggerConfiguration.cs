using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace IDE.Services
{
    internal sealed class FileLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, string> LogLevelToStringMap { get; set; } = new Dictionary<LogLevel, string>()
        {
            [LogLevel.None] = "None",
            [LogLevel.Information] = "Info",
            [LogLevel.Debug] = "Debug",
            [LogLevel.Warning] = "Warn",
            [LogLevel.Error] = "Error",
            [LogLevel.Trace] = "Trace"
        };
    }
}
