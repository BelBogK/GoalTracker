using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }

        public virtual ICollection<PlanItem> PlanItems { get; set; } = new List<PlanItem>();

    }
}
