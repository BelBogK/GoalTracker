using GoalTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface ITaskItemRepository : IBaseRepo<TaskItem>
    {
        Task<List<TaskItem>> GetTasksForProject(int projectId, string userId);
        Task<TaskItem> CreateAsync(TaskItem entity, int projectId);
    }
}
