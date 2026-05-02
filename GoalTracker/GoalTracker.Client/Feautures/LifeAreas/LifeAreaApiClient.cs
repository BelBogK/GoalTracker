using GoalTracker.Shared;
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
    }
}
