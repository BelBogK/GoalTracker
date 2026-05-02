using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class GoalScenario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<Goal> Goals { get; set; } = [];
        public virtual ICollection<Event> Events { get; set; }=new List<Event>(); 
    }
}
