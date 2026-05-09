using GoalTracker.Shared;
using GoalTracker.Shared.SuperClass;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.Tasks
{
    public class TaskItemApiClient(HttpClient http)
    {
        public async Task<List<TaskItemDTO>> GetTasksByProjectAsync(int projectId)
        {
            return await http.GetFromJsonAsync<List<TaskItemDTO>>($"/api/project/{projectId}/tasks") ?? [];
        }

        public async Task<TaskItemDTO> CreateTaskAsync(int projectId, TaskItemDTO taskItem)
        {
            var response = await http.PostAsJsonAsync($"/api/project/{projectId}/tasks", taskItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItemDTO>();
        }
        public async Task<TaskItemDTO> GetTaskByIdAsync(int id)
        {
            var response = await http.GetAsync($"/api/tasks/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItemDTO>();
        }
        public async Task<TaskItemDTO> UpdateTaskAsync(TaskItemDTO taskItem)
        {
            var response = await http.PutAsJsonAsync($"/api/tasks/{taskItem.Id}", taskItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItemDTO>();
        }
        public async Task DeleteTaskAsync(int taskId)
        {
            var r = await http.DeleteAsync($"/api/tasks/{taskId}");
        }

        public async Task<List<TaskHierarchyLifeAreaDTO>> GetDailyTrackerAsync(
        DateTime startTime, DateTime endTime)
        {
            var start = startTime.ToString("yyyy-MM-ddTHH:mm:ss");
            var end = endTime.ToString("yyyy-MM-ddTHH:mm:ss");
            return await http.GetFromJsonAsync<List<TaskHierarchyLifeAreaDTO>>(
                $"/api/tasks/daily-tracker?startTime={start}&endTime={end}") ?? [];
        }

        public async Task<List<TaskHierarchyLifeAreaDTO>> GetNonTrackedAsync()
        {
            return await http.GetFromJsonAsync<List<TaskHierarchyLifeAreaDTO>>(
                "/api/tasks/non-tracked") ?? [];
        }

        public async Task AddToTrackerAsync(int taskId, DateTime startTime, DateTime endTime)
        {
            var response = await http.PostAsJsonAsync($"/api/tasks/{taskId}/tracker", new
            {
                startTime,
                endTime
            });
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveFromTrackerAsync(int taskId)
        {
            await http.DeleteAsync($"/api/tasks/{taskId}/tracker");
        }

    }
}
