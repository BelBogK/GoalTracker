using GoalTracker.Shared;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.Lists
{
    public class DoItListApiClient(HttpClient http)
    {
        public async Task<List<DoItListDTO>> GetAllAsync()
            => await http.GetFromJsonAsync<List<DoItListDTO>>("/api/do-it-lists") ?? [];

        public async Task<DoItListDTO?> GetByIdAsync(int id)
            => await http.GetFromJsonAsync<DoItListDTO>($"/api/do-it-lists/{id}");

        public async Task<int> CreateAsync(DoItListDTO dto)
        {
            var r = await http.PostAsJsonAsync("/api/do-it-lists", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<int>();
        }

        public async Task<DoItListDTO> UpdateAsync(DoItListDTO dto)
        {
            var r = await http.PutAsJsonAsync($"/api/do-it-lists/{dto.Id}", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<DoItListDTO>() ?? dto;
        }

        public async Task DeleteAsync(int id)
            => await http.DeleteAsync($"/api/do-it-lists/{id}");

        public async Task<int> AddItemAsync(int listId, DoItListItemDTO dto)
        {
            var r = await http.PostAsJsonAsync($"/api/do-it-lists/{listId}/items", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<int>() ;
        }

        public async Task<DoItListItemDTO> UpdateItemAsync(int listId, DoItListItemDTO dto)
        {
            var r = await http.PutAsJsonAsync($"/api/do-it-lists/{listId}/items/{dto.Id}", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<DoItListItemDTO>() ?? dto;
        }

        public async Task DeleteItemAsync(int listId, int itemId)
            => await http.DeleteAsync($"/api/do-it-lists/{listId}/items/{itemId}");
    }
}
