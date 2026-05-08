using GoalTracker.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class GoalScenario: BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ExpertFlow { get; set; }
        public int? SuccessProbability { get; set; }
        public string? Constraints {  get; set; }
        public string? PotentialProblems { get; set; }
        public bool IsOprimisticScenarios { get; set; } = true;
        public virtual ICollection<GoalScenarioRelation> ChildRelations { get; set; } = [];
        public virtual ICollection<GoalScenarioRelation> ParentRelations { get; set; } = [];
        public bool IsActive { get; set; }

        public virtual ICollection<Goal> Goals { get; set; } = [];
        public virtual ICollection<Event> Events { get; set; }=new List<Event>(); 
        public virtual ICollection<Project> Projects { get; set;} = new List<Project>();

    }
}
