using TareasApi.Application.DTOs;
using TareasApi.Application.Interfaces;
using TareasApi.Domain.Entities;
using TareasApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TareasApi.Application.Services
{
    public class TareaService : ITareaService
    {

        private readonly TareasContext _context;

        public TareaService(TareasContext context)
        {
            _context = context;
        }

        public async Task<Tarea> GetTarea(int tareaId)
        {
            return await _context.Tareas.FindAsync(tareaId);
        }

        public async Task<bool> AddTareaAsync(TareaDto tareaDto)
        {
            

            var tarea = new Tarea

            {
                Titulo = tareaDto.Titulo,
                Descripcion = tareaDto.Descripcion ?? string.Empty,
                FechaCreacion = tareaDto.FechaCreacion,
                FechaVencimiento = tareaDto.FechaVencimiento,
                Completada = tareaDto.Completada,
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TareaDto>> GetTareasAsync(int pageNumber, int pageSize)
        {

            var resultados = await _context.Tareas
               .Select(d => new  TareaDto
               {
                   Id = d.Id,
                   Titulo = d.Titulo,
                   Descripcion = d.Descripcion,
                   FechaCreacion = d.FechaCreacion,
                   FechaVencimiento = d.FechaVencimiento,
                   Completada= d.Completada
               })
               .AsNoTracking()
               .ToListAsync();

            return resultados
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

        }
        public async Task<bool> UpdateTareaAsync(TareaDto tareaDto)
        {

            var tarea = await _context.Tareas.FindAsync(tareaDto.Id);

            if (tarea == null)
            {
                return false;
            }


            tarea.Titulo = tareaDto.Titulo;
            tarea.Descripcion = tareaDto.Descripcion ?? tarea.Descripcion;
            tarea.FechaCreacion = tareaDto.FechaCreacion;
            tarea.FechaVencimiento = tareaDto.FechaVencimiento;
            tarea.Completada = tareaDto.Completada;
            
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTareaAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return false; // Si la tarea no se encuentra, devolvemos false
            }

            _context.Tareas.Remove(tarea); // Removemos la tarea
            await _context.SaveChangesAsync(); // Guardamos los cambios

            return true; // Devolvemos true si la tarea se eliminó correctamente
        }

    }
}
