using GoalTracker.Shared;
using GoalTracker.Shared.SuperClass;

namespace GoalTracker.Features.Extensions
{
    public static class TaskHierarchyMappingExtensions
    {
        public static TaskHierarchyLifeAreaDTO ToTaskHierarchyDto(this GoalTracker.Domain.Entities.LifeArea lifeArea)
        {
            return new TaskHierarchyLifeAreaDTO
            {
                Id = lifeArea.Id,
                Name = lifeArea.Name,

                Goals = lifeArea.Goals?
                    .Select(g => g.ToTaskHierarchyDto())
                    .ToList() ?? []
            };
        }

        public static TaskHierarchyGoalDTO ToTaskHierarchyDto(this GoalTracker.Domain.Entities.Goal goal)
        {
            return new TaskHierarchyGoalDTO
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
                CurrentStatus = goal.CurrentStatus,
                GoalType = goal.GoalType,
                Priority = goal.Priority,

                Scenarios = goal.Scenarios?
                    .Select(s => s.ToTaskHierarchyDto())
                    .ToList() ?? [],

                Projects = goal.Projects?
                    .Select(p => p.ToTaskHierarchyDto())
                    .ToList() ?? []
            };
        }

        public static TaskHierarchyScenarioDTO ToTaskHierarchyDto(this GoalTracker.Domain.Entities.GoalScenario scenario)
        {
            return new TaskHierarchyScenarioDTO
            {
                Id = scenario.Id,
                Name = scenario.Name,
                Description = scenario.Description,
                IsActive = scenario.IsActive,
                IsOprimisticScenarios = scenario.IsOprimisticScenarios,

                Children = scenario.ChildRelations?
                    .Where(x => x.Child != null)
                    .Select(x => x.Child.ToTaskHierarchyDto())
                    .ToList() ?? [],

                Projects = scenario.Projects?
                    .Select(p => p.ToTaskHierarchyDto())
                    .ToList() ?? []
            };
        }

        public static TaskHierarchyProjectDTO ToTaskHierarchyDto(this GoalTracker.Domain.Entities.Project project)
        {
            return new TaskHierarchyProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CurrentStatus = project.CurrentStatus,
                Priority = project.Priority,

                Tasks = project.TaskItems?
                    .Select(t => t.ToDto())
                    .ToList() ?? []
            };
        }

        public static TaskItemDTO ToDto(this GoalTracker.Domain.Entities.TaskItem task)
        {
            return new TaskItemDTO
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Effort = task.Effort,
                Procents = task.Procents,
                CurrentStatus = task.CurrentStatus,
                StartAt= task.StartAt,
                Path=task.Path
            };
        }

        public static GoalTracker.Domain.Entities.TaskItem ToEntity(this TaskItemDTO task)
        {
            return new GoalTracker.Domain.Entities.TaskItem
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Effort = task.Effort,
                Procents = task.Procents,
                CurrentStatus = task.CurrentStatus,
                StartAt = task.StartAt
            };
        }
    }
}
