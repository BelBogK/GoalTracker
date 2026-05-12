using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;
using GoalTracker.Shared.Path;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalTracker.Domain.Entities
{
    public class Project : BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Result { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public ProjectType GoalType { get; set; }
        public Priority Priority { get; set; }
        public int? ParentProjectId { get; set; }
        public virtual ICollection<Project> NextProjects { get; set; } = new List<Project>();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? FinishedAt { get; set; } 
        public int PointsForCompletedProject { get; set; }
        public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
        public virtual ICollection<TaskItem> TaskItems { get; set; }
        [NotMapped]
        public List<EntityPathDto> Path { get; set; }
    }
}
