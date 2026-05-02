using GoalTracker.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class Goal: BaseWithUser
    {
        public int Id { get; set; }
        public string Description { get; set; }//SMART goal description
        public DateTime? TargetDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string Reward { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public GoalType GoalType { get; set; }
        public Priority Priority { get; set; }
        public string IdealVision { get; set; }
        public virtual ICollection<GoalScenario> Scenarios { get; set; } = new List<GoalScenario>();
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<LifeArea> LifeAreas { get; set; }= new List<LifeArea>();
    }
}
