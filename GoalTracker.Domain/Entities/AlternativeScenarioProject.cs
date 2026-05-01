namespace GoalTracker.Domain.Entities
{
    public class AlternativeScenarioProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Probability of this scenario to happen, based on past data and current trends. This can be a percentage value (0-100)
        /// </summary>
        public int Probability { get; set; } = 0;
        /// <summary>
        /// Desirability of this scenario, based on how well it aligns with the user's goals and preferences. This can be a score (e.g., 0-10)
        /// </summary>
        public int Desirability { get; set; } = 0;
        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}