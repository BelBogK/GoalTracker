using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class GoalScenarioDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsOprimisticScenarios { get; set; }
        public List<GoalScenarioDTO> ChildRelations { get; set; } = [];
        public List<ProjectDTO> Projects { get; set; } = [];
    }
}
