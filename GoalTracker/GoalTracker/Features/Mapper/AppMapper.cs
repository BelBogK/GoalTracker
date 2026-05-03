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

        [MapperIgnoreSource(nameof(GoalTracker.Domain.Entities.Project.NextProjects))] 
        public partial ProjectDTO ToDto(GoalTracker.Domain.Entities.Project entity);
        [MapperIgnoreSource(nameof(GoalTracker.Domain.Entities.Project.NextProjects))] 
        public partial GoalTracker.Domain.Entities.Project ToEntity(ProjectDTO dto);
        public partial TaskItemDTO ToDto(GoalTracker.Domain.Entities.TaskItem entity);
        public partial GoalTracker.Domain.Entities.TaskItem ToEntity(TaskItemDTO dto); 

    }
}
