using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareasApi.Domain.Entities;

namespace TareasApi.Infrastructure.Persistence
{
    public class TareasContext : DbContext
    {

        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<LogEntry> Logs { get; set; }

    }
}
