using System.Security.Cryptography.X509Certificates;

namespace GoalTracker.Domain.Entities
{
    public class PlanItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }
        public int ItemOrder { get; set; } 
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}