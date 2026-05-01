using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class ProjectScenario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GoalId { get; set; }
        public virtual Goal Goal { get; set; }
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
