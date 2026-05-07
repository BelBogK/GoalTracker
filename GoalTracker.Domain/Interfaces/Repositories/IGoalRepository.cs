using GoalTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface IGoalRepository : IBaseRepo<Goal>
    {
        Task<IEnumerable<Goal>> GetByLifeAreaAsync(string userId, int lifeAreaId);
    }
}
