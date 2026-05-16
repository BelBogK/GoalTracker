using GoalTracker.Shared.Enums;
using GoalTracker.Shared.ImproveWorking;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.Inprove
{
    public class ExecutionImprovementApiClient(HttpClient http)
    {
        public async Task<ExecutionImprovementDTO> CreateAsync(ExecutionImprovementDTO dto)
        {
            var r = await http.PostAsJsonAsync("/api/execution-improvements", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<ExecutionImprovementDTO>() ?? dto;
        }
        public async Task<List<ExecutionImprovementDTO>> GetByEntitysAsync(int entityId, TrackedEntitiesType type)
       => await http.GetFromJsonAsync<List<ExecutionImprovementDTO>>(
           $"/api/execution-improvements/by-entity/{entityId}?type={(int)type}") ?? [];

        public async Task<ExecutionImprovementDTO?> GetByIdAsync(int id)
            => await http.GetFromJsonAsync<ExecutionImprovementDTO>($"/api/execution-improvements/{id}");

        public async Task<ExecutionImprovementDTO?> GetByEntityAsync(int entityId, TrackedEntitiesType type)
            => await http.GetFromJsonAsync<ExecutionImprovementDTO>(
                $"/api/execution-improvements/entity/{entityId}?type={type}");

        public async Task<List<ExecutionImprovementDTO>> GetAllAsync()
            => await http.GetFromJsonAsync<List<ExecutionImprovementDTO>>("/api/execution-improvements") ?? [];

        public async Task<HistoreImprovedDTO> AddHistoryAsync(int improvementId, HistoreImprovedDTO dto)
        {
            var r = await http.PostAsJsonAsync($"/api/execution-improvements/{improvementId}/history", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<HistoreImprovedDTO>() ?? dto;
        }

        public async Task<HistoreImprovedDTO> UpdateHistoryAsync(int improvementId, HistoreImprovedDTO dto)
        {
            var r = await http.PutAsJsonAsync($"/api/execution-improvements/{improvementId}/history/{dto.Id}", dto);
            r.EnsureSuccessStatusCode();
            return await r.Content.ReadFromJsonAsync<HistoreImprovedDTO>() ?? dto;
        }

        public async Task DeleteHistoryAsync(int improvementId, int historyId)
            => await http.DeleteAsync($"/api/execution-improvements/{improvementId}/history/{historyId}");
    }
}
