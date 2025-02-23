using WebTareas.Models;
using System.Net.Http.Json;

namespace WebTareas.Services
{
    public class TareaService
    {
        private readonly HttpClient _httpClient;

        public TareaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Tarea>> ObtenerTareasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Tarea>>("api/tareas");
        }

        public async Task<Tarea> ObtenerTareaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tarea>($"api/tareas/{id}");
        }

        public async Task CrearTareaAsync(Tarea tarea)
        {
            await _httpClient.PostAsJsonAsync("api/tareas", tarea);
        }

        public async Task ActualizarTareaAsync(Tarea tarea)
        {
            await _httpClient.PutAsJsonAsync($"api/tareas", tarea);
        }

        public async Task<bool> EliminarTareaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tareas/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
