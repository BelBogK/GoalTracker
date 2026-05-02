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

        public partial ProjectDTO ToDto(GoalTracker.Domain.Entities.Project entity);
        public partial GoalTracker.Domain.Entities.Project ToEntity(ProjectDTO dto);

    }
}
