using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;

namespace GoalTracker.Domain.Entities.ImproveWorking
{
    public class HistoreImproved:BaseWithUser
    {
        public int Id { get; set; }
        public DateTime StartImprove { get; set; }
        public string? Comments { get; set; }
        public string? Result { get; set; }
        public bool? IsImproved { get; set; }
        public TrackedEntitiesType TrackedEntitiesType { get; set;  }
        public int EntityeId { get; set; }
    }
}