using GoalTracker.Shared;
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

        public async Task<TaskItemDTO> UpdateTaskAsync(TaskItemDTO taskItem)
        {
            // Implementation for updating an existing task
            return new TaskItemDTO();
        }
        public async Task DeleteTaskAsync(int taskId)
        {
            var r = await http.DeleteAsync($"/api/tasks/{taskId}");
        }
    }
}
