using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Services;
using GoalTracker.Shared.Enums;
using GoalTracker.Shared.Path;
using Microsoft.EntityFrameworkCore;
namespace GoalTracker.Infrastructure.Services
{

    public class PathBuilderService : IPathBuilderService
    {
        private readonly ApplicationDbContext _db;

        public PathBuilderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<EntityPathDto>> BuildPathsAsync(
            int entityId,
            PathEntityType type)
        {
            return type switch
            {
                PathEntityType.Task =>
                    await BuildTaskPaths(entityId),

                PathEntityType.Project =>
                    await BuildProjectPaths(entityId),

                PathEntityType.Scenario =>
                    await BuildScenarioPaths(entityId),

                PathEntityType.Goal =>
                    await BuildGoalPaths(entityId),

                PathEntityType.LifeArea =>
                    await BuildLifeAreaPaths(entityId),

                _ => []
            };
        }

        // =========================================================
        // TASK
        // =========================================================

        private async Task<List<EntityPathDto>> BuildTaskPaths(int taskId)
        {
            var task = await _db.TaskItems
                .Include(x => x.Projects)
                    .ThenInclude(x => x.Goals)
                        .ThenInclude(x => x.LifeAreas)
                .Include(x => x.Projects)
                    .ThenInclude(x => x.Goals)
                        .ThenInclude(x => x.Scenarios)
                .FirstOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
                return [];

            var result = new List<EntityPathDto>();

            foreach (var project in task.Projects)
            {
                foreach (var goal in project.Goals)
                {
                    // Goal directly
                    foreach (var lifeArea in goal.LifeAreas)
                    {
                        result.Add(new EntityPathDto
                        {
                            Nodes =
                            [
                                CreateNode(lifeArea),
                            CreateNode(goal),
                            CreateNode(project),
                            CreateNode(task)
                            ]
                        });
                    }

                    // Through scenarios
                    foreach (var scenario in goal.Scenarios)
                    {
                        foreach (var lifeArea in goal.LifeAreas)
                        {
                            result.Add(new EntityPathDto
                            {
                                Nodes =
                                [
                                    CreateNode(lifeArea),
                                CreateNode(goal),
                                CreateNode(scenario),
                                CreateNode(project),
                                CreateNode(task)
                                ]
                            });
                        }
                    }
                }
            }

            return result;
        }

        // =========================================================
        // PROJECT
        // =========================================================

        private async Task<List<EntityPathDto>> BuildProjectPaths(int projectId)
        {
            var project = await _db.Projects
                .Include(x => x.Goals)
                    .ThenInclude(x => x.LifeAreas)
                .Include(x => x.Goals)
                    .ThenInclude(x => x.Scenarios)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project == null)
                return [];

            var result = new List<EntityPathDto>();

            foreach (var goal in project.Goals)
            {
                foreach (var lifeArea in goal.LifeAreas)
                {
                    result.Add(new EntityPathDto
                    {
                        Nodes =
                        [
                            CreateNode(lifeArea),
                        CreateNode(goal),
                        CreateNode(project)
                        ]
                    });
                }

                foreach (var scenario in goal.Scenarios)
                {
                    foreach (var lifeArea in goal.LifeAreas)
                    {
                        result.Add(new EntityPathDto
                        {
                            Nodes =
                            [
                                CreateNode(lifeArea),
                            CreateNode(goal),
                            CreateNode(scenario),
                            CreateNode(project)
                            ]
                        });
                    }
                }
            }

            return result;
        }

        // =========================================================
        // SCENARIO
        // =========================================================

        private async Task<List<EntityPathDto>> BuildScenarioPaths(int scenarioId)
        {
            var scenario = await _db.GoalScenarios
                .Include(x => x.Goals)
                    .ThenInclude(x => x.LifeAreas)
                .FirstOrDefaultAsync(x => x.Id == scenarioId);

            if (scenario == null)
                return [];

            var result = new List<EntityPathDto>();

            foreach (var goal in scenario.Goals)
            {
                foreach (var lifeArea in goal.LifeAreas)
                {
                    result.Add(new EntityPathDto
                    {
                        Nodes =
                        [
                            CreateNode(lifeArea),
                        CreateNode(goal),
                        CreateNode(scenario)
                        ]
                    });
                }
            }

            return result;
        }

        // =========================================================
        // GOAL
        // =========================================================

        private async Task<List<EntityPathDto>> BuildGoalPaths(int goalId)
        {
            var goal = await _db.Goals
                .Include(x => x.LifeAreas)
                .FirstOrDefaultAsync(x => x.Id == goalId);

            if (goal == null)
                return [];

            return goal.LifeAreas
                .Select(lifeArea => new EntityPathDto
                {
                    Nodes =
                    [
                        CreateNode(lifeArea),
                    CreateNode(goal)
                    ]
                })
                .ToList();
        }

        // =========================================================
        // LIFE AREA
        // =========================================================

        private async Task<List<EntityPathDto>> BuildLifeAreaPaths(int lifeAreaId)
        {
            var lifeArea = await _db.LifeAreas
                .FirstOrDefaultAsync(x => x.Id == lifeAreaId);

            if (lifeArea == null)
                return [];

            return
            [
                new EntityPathDto
            {
                Nodes =
                [
                    CreateNode(lifeArea)
                ]
            }
            ];
        }

        // =========================================================
        // NODE FACTORY
        // =========================================================

        private static PathNodeDto CreateNode(LifeArea x) => new()
        {
            Id = x.Id,
            Name = x.Name,
            Type = PathEntityType.LifeArea
        };

        private static PathNodeDto CreateNode(Goal x) => new()
        {
            Id = x.Id,
            Name = x.Name,
            Type = PathEntityType.Goal
        };

        private static PathNodeDto CreateNode(GoalScenario x) => new()
        {
            Id = x.Id,
            Name = x.Name,
            Type = PathEntityType.Scenario
        };

        private static PathNodeDto CreateNode(Project x) => new()
        {
            Id = x.Id,
            Name = x.Name,
            Type = PathEntityType.Project
        };

        private static PathNodeDto CreateNode(TaskItem x) => new()
        {
            Id = x.Id,
            Name = x.Name,
            Type = PathEntityType.Task
        };
    }
}
