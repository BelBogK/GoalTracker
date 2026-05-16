using GoalTracker.Shared.Enums;

namespace GoalTracker.Shared.ImproveWorking
{
    public class HistoreImprovedDTO
    {
        public int Id { get; set; }
        public DateTime StartImprove { get; set; }
        public string? Comments { get; set; }
        public string? Result { get; set; }
        public bool? IsImproved { get; set; }
        public TrackedEntitiesType TrackedEntitiesType { get; set; }
        public int EntityeId { get; set; }
    }
}