using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using Riok.Mapperly.Abstractions;

namespace GoalTracker.Features.Mapper
{
    [Mapper]
    public partial class AppMapper
    {
        public partial GoalDTO ToDto(GoalTracker.Domain.Entities.Goal entity);
        public partial GoalTracker.Domain.Entities.Goal ToEntity(GoalDTO dto);

        public partial LifeAreaDTO ToDto(GoalTracker.Domain.Entities.LifeArea entity);
        public partial GoalTracker.Domain.Entities.LifeArea ToEntity(LifeAreaDTO dto);

        [MapperIgnoreSource(nameof(GoalTracker.Domain.Entities.Project.NextProjects))]
        public partial ProjectDTO ToDto(GoalTracker.Domain.Entities.Project entity);
        [MapperIgnoreSource(nameof(GoalTracker.Domain.Entities.Project.NextProjects))]
        public partial GoalTracker.Domain.Entities.Project ToEntity(ProjectDTO dto);

        public partial TaskItemDTO ToDto(GoalTracker.Domain.Entities.TaskItem entity);
        public partial GoalTracker.Domain.Entities.TaskItem ToEntity(TaskItemDTO dto);

        [MapperIgnoreSource(nameof(GoalTracker.Domain.Entities.GoalScenario.ChildRelations))]
        [MapperIgnoreSource(nameof(GoalTracker.Domain.Entities.GoalScenario.ParentRelations))]
        public partial GoalScenarioDTO ToDtoFlat(GoalTracker.Domain.Entities.GoalScenario entity);

        public partial GoalScenarioDTO ToDTO(GoalScenarioRelation entity);

        public partial GoalTracker.Domain.Entities.GoalScenario ToEntity(GoalScenarioDTO dto);
        
        public GoalScenarioDTO ToDto(GoalTracker.Domain.Entities.GoalScenario entity)
        {
            var dto = ToDtoFlat(entity);

            dto.ChildRelations = entity.ChildRelations?
                .Where(r => r.Child != null)
                .Select(r => ToDto(r.Child))
                .ToList() ?? [];

            return dto;
        }
    }
}