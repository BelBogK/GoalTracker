using GoalTracker.Features.Mapper;
using GoalTracker.Shared;

namespace GoalTracker.Features.Extensions
{ 
    public static class GoalMappingExtensions
    {
        public static GoalScenarioDTO ToDto(this GoalTracker.Domain.Entities.GoalScenario sc, AppMapper mapper) =>
            new()
            {
                Description = sc.Description,
                Id = sc.Id,
                IsActive = sc.IsActive,
                Name = sc.Name,
                Projects = sc.Projects.Select(mapper.ToDto).ToList(),
                ChildRelations = sc.ChildRelations
                    .Select(cr => cr.Child.ToDto(mapper))
                    .ToList()
            };

        public static GoalDTO ToDto(this GoalTracker.Domain.Entities.Goal goal, AppMapper mapper) =>
            new()
            {
                Description = goal.Description,
                StartDate = goal.StartDate,
                TargetDate = goal.TargetDate,
                CurrentStatus = goal.CurrentStatus,
                GoalType = goal.GoalType,
                Id = goal.Id,
                IdealVision = goal.IdealVision,
                Name = goal.Name,
                Priority = goal.Priority,
                Reward = goal.Reward,
                Scenarios = goal.Scenarios
                    .Select(sc => sc.ToDto(mapper))
                    .ToList()
            };
    }
}
