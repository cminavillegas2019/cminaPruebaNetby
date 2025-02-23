using TareasApi.Domain.Entities;
using TareasApi.Infrastructure.Interfaces;
using TareasApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasApi.Infrastructure.Logging
{
    public class DatabaseLoggerService : ILoggerService
    {
        private readonly TareasContext  _context;

        public DatabaseLoggerService(TareasContext context)
        {
            _context = context;
        }

        public void Log(string message)
        {
            var logEntry = new LogEntry { Message = message };
            _context.Logs.Add(logEntry);
            _context.SaveChanges();
        }
    }
}
