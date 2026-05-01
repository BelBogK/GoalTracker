namespace GoalTracker.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
        public virtual ICollection<AlternativeScenarioProject> Alternatives { get; set; } = new List<AlternativeScenarioProject>();

    }
}