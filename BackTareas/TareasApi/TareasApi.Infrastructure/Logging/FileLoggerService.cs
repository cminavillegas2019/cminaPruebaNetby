using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareasApi.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DeportesApi.Infrastructure.Logging
{
    public class FileLoggerService : ILoggerService
    {
        private readonly string _logFilePath;

        public FileLoggerService(IConfiguration configuration)
        {
            _logFilePath = configuration["Logging:FilePath"] ?? "logs.txt";
        }

        public void Log(string message)
        {
            var logMessage = $"{DateTime.UtcNow}: {message}{Environment.NewLine}";
            File.AppendAllText(_logFilePath, logMessage);
        }
    }
}
