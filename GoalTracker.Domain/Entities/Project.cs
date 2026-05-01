using GoalTracker.Domain.Entities.Base;

namespace GoalTracker.Domain.Entities
{
    public class Project: BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
