using GoalTracker.Shared;
using GoalTracker.Shared.SuperClass;
using System.Net.Http.Json;
using static MudBlazor.CategoryTypes;

namespace GoalTracker.Client.Feautures.LifeAreas
{
    public class LifeAreaApiClient(HttpClient http)
    {
        public async Task<List<LifeAreaDTO>> GetLifeAreasAsync()
        {
            return await http.GetFromJsonAsync<List<LifeAreaDTO>>("/api/lifeareas") ?? [];
        }

        public async Task<List<LifeAreaDTO>> SaveToPersonalArea(IEnumerable<LifeAreaDTO> lifeAreas)
        {
            var response = await http.PostAsJsonAsync("/api/lifeareas", lifeAreas);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LifeAreaDTO>>() ?? [];
        }

        public async Task<Dictionary<string, int>> GetLifeAreaWithPoints(DateTime startTime, DateTime endTime)
        {
            var start = startTime.ToString("yyyy-MM-ddTHH:mm:ss");
            var end = endTime.ToString("yyyy-MM-ddTHH:mm:ss");
            return await http.GetFromJsonAsync<Dictionary<string,int>>(
                $"/api/lifeareas/AreaWithPoints?startTime={start}&endTime={end}") ?? [];  
        }
        public async Task<Dictionary<string, int>> GetLifeAreasWithPotentialPoints(DateTime startTime, DateTime endTime)
        {
            var start = startTime.ToString("yyyy-MM-ddTHH:mm:ss");
            var end = endTime.ToString("yyyy-MM-ddTHH:mm:ss");
            return await http.GetFromJsonAsync<Dictionary<string, int>>(
                $"/api/lifeareas/AreaWithPotentialPoints?startTime={start}&endTime={end}") ?? [];
        }

        public async Task<LifeAreaDTO?> GetByIdAsync(int id)
    => await http.GetFromJsonAsync<LifeAreaDTO>($"/api/lifeareas/{id}");

        public async Task<List<GoalDTO>> GetGoalsAsync(int lifeAreaId)
            => await http.GetFromJsonAsync<List<GoalDTO>>($"/api/lifeareas/{lifeAreaId}/goals") ?? [];
    }
}
