using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared.ImproveWorking
{
    public class ExecutionImprovementDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<HistoreImprovedDTO> HistoreImproveds { get; set; } = [];
    }
}
