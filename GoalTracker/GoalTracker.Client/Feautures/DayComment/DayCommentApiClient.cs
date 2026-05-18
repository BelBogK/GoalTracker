using GoalTracker.Shared;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.DayComment
{ 
    public class DayCommentApiClient
    {
        private readonly HttpClient _httpClient;

        public DayCommentApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DayCommentDTO>> GetCommentsForPeriodAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _httpClient.GetFromJsonAsync<List<DayCommentDTO>>($"api/daycomment/period?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}") ?? new();
        }

        public async Task SaveCommentAsync(DayCommentDTO dto)
        {
            await _httpClient.PostAsJsonAsync("api/daycomment", dto);
        }
    }
}
