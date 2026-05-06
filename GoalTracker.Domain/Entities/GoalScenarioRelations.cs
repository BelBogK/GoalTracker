using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class GoalScenarioRelation
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public virtual GoalScenario Parent { get; set; } = null!;
        public int ChildId { get; set; }
        public virtual GoalScenario Child { get; set; } = null!;
    }
}
