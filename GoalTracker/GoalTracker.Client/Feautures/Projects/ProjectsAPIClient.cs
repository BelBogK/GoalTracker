using GoalTracker.Shared;
using System.Net.Http.Json;

namespace GoalTracker.Client.Feautures.Projects
{
    public class ProjectsAPIClient(HttpClient http)
    {
        public async Task<List<ProjectDTO>> GetProjectsAsync()
        {
            return await http.GetFromJsonAsync<List<ProjectDTO>>("/api/projects") ?? [];
        }
        public async Task<List<ProjectDTO>> GetProjectsForGoalAsync(int goalId)
        {
            return await http.GetFromJsonAsync<List<ProjectDTO>>($"/api/goals/{goalId}/projects") ?? [];
        } 
        public async Task<ProjectDTO> SaveProject(ProjectDTO project, int goalId)
        {
            var response = await http.PostAsJsonAsync($"/api/goals/{goalId}/projects", project);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProjectDTO>();
        }
        public async Task<ProjectDTO> SaveProject(ProjectDTO project)
        {
            var response = await http.PostAsJsonAsync("/api/projects", project);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProjectDTO>();
        }

        public async Task<ProjectDTO?> GetProjectByIdAsync(int id)
        {
            return await http.GetFromJsonAsync<ProjectDTO>($"/api/projects/{id}");
        }

        public async Task UpdateProjectAsync(ProjectDTO project)
        {
            var response = await http.PutAsJsonAsync($"/api/projects/{project.Id}", project);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddGoalToProjectAsync(int projectId, int goalId)
        {
            var response = await http.PostAsync($"/api/projects/{projectId}/goals/{goalId}", null);
            response.EnsureSuccessStatusCode();
        }
        public async Task RemoveGoalFromProjectAsync(int projectId, int goalId)
        {
            await http.DeleteAsync($"/api/projects/{projectId}/goals/{goalId}");
        }
    }
}
