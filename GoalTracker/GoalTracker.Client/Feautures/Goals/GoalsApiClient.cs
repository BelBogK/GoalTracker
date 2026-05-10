using GoalTracker.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GoalTracker.Client.Feautures.Goals
{
    public class GoalsApiClient(HttpClient http)
    {
        public async Task<List<GoalDTO>> GetGoalsAsync()
        {
            return await http.GetFromJsonAsync<List<GoalDTO>>("/api/goals") ?? [];
        }

        public async Task<GoalDTO> SaveGoal(GoalDTO goal)
        {
            var response = await http.PostAsJsonAsync("/api/goals", goal);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GoalDTO>();
        }

        public async Task<GoalDTO> UpdateGoal(GoalDTO goal)
        {
            var response = await http.PutAsJsonAsync("/api/goals", goal);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GoalDTO>();
        }
    }
}
