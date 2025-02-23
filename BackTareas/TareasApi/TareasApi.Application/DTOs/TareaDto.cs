using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasApi.Application.DTOs
{
    public class TareaDto
    {
    
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Completada { get; set; }
     

    }
}
