using GoalTracker.Shared;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.LifeAreas
{
    public class LifeAreaApiClient(HttpClient http)
    {
        public async Task<List<LifeAreaDTO>> GetLifeAreasAsync()
        {
            return await http.GetFromJsonAsync<List<LifeAreaDTO>>("/api/lifeareas") ?? [];
        }
    }
}
