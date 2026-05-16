using GoalTracker.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities.ImproveWorking
{
    public class ExecutionImprovement:BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<HistoreImproved> HistoreImproveds { get; set; } = [];
    }
}
