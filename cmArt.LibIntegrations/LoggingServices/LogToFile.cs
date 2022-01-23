using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.IO;


namespace cmArt.LibIntegrations.LoggingServices
{
    public class LogToFile : ILogger
    {
        private StreamWriter _file;
        private readonly string _name;
        private string _LogFilePathAndName;
        public LogToFile()
        {
        }
        public void Init(string LogFilePathAndName, bool append = true)
        {
            _LogFilePathAndName = LogFilePathAndName;
            if (!append)
            {
                _file = new(_LogFilePathAndName, append: false);
                _file.WriteLine($"{DateTime.Now}");
                _file.Close();
            }
        }
        public IDisposable BeginScope<TState>(TState state) => default!;
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _file = new(_LogFilePathAndName, append: true);
            //_file.WriteLine($"logLevel: {logLevel}, eventId: {eventId}, state: {state}");
            _file.WriteLine($"Info: {state}");
            _file.Close();
        }
    }

}
