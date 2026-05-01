using GoalTracker.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class LifeArea: BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdealVision { get; set; }
        public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}
