using GoalTracker.Shared.Enums;
using GoalTracker.Shared.Path;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Services
{
    public interface IPathBuilderService
    {
        Task<List<EntityPathDto>> BuildPathsAsync(
            int entityId,
            PathEntityType type);
    }
}
