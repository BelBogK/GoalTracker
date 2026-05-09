using GoalTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface IGoalScenarioRepository : IBaseRepo<GoalScenario>
    {
        Task<GoalScenario> CreateAsync(GoalScenario scenario, int goalId);
        Task<IEnumerable<GoalScenario>> GetByGoalIdAsync(int goalId);
        Task<GoalScenario?> GetByIdWithChildrenAsync(int id);
        Task<GoalScenario> RevertIsActiveScen(int scenId, string userId);
    }

}
