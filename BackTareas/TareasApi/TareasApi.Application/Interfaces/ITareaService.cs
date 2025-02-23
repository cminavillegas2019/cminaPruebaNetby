using TareasApi.Domain.Entities;
using TareasApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasApi.Application.Interfaces
{
    public interface ITareaService
    {

        Task<Tarea> GetTarea(int tareaId);
        Task<bool> AddTareaAsync(TareaDto tareaDto);
        Task<IEnumerable<TareaDto>> GetTareasAsync(int pageNumber, int pageSize);
        Task<bool> UpdateTareaAsync(TareaDto tareaDto);
        Task<bool> DeleteTareaAsync(int tareaId);
    }
}
