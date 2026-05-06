using GoalTracker.Shared;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.GoalsScenario
{
    public class GoalScenarioApiClient(HttpClient http)
    {
        public async Task<List<GoalScenarioDTO>> GetByGoalAsync(int goalId)
            => await http.GetFromJsonAsync<List<GoalScenarioDTO>>(
                $"/api/goals/{goalId}/scenarios") ?? [];

        public async Task<GoalScenarioDTO?> AddToGoalAsync(int goalId, GoalScenarioDTO scenario)
        {
            var response = await http.PostAsJsonAsync($"/api/goals/{goalId}/scenarios", scenario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GoalScenarioDTO>();
        }

        public async Task<GoalScenarioDTO?> AddChildAsync(int parentId, GoalScenarioDTO child)
        {
            var response = await http.PostAsJsonAsync($"/api/scenarios/{parentId}/children", child);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GoalScenarioDTO>();
        }

        public async Task<GoalScenarioDTO?> UpdateAsync(GoalScenarioDTO scenario)
        {
            var response = await http.PutAsJsonAsync($"/api/scenarios/{scenario.Id}", scenario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GoalScenarioDTO>();
        }

        public async Task DeleteAsync(int id)
            => await http.DeleteAsync($"/api/scenarios/{id}");
    }
}
