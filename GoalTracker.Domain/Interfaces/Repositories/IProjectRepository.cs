using GoalTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface IProjectRepository : IBaseRepo<Project>
    {
        Task<IEnumerable<Project>> GetByGoalAsync(string userId, int goalId);
        Task<Project> CreateAsync(Project entity, int goalId);
    }
}
