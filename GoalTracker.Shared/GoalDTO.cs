using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class GoalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }//SMART goal description
        public DateTime? TargetDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string Reward { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public GoalType GoalType { get; set; }
        public Priority Priority { get; set; }
        public string IdealVision { get; set; }
        public List<GoalScenarioDTO> Scenarios { get; set; } = new List<GoalScenarioDTO>(); 
        public List<LifeAreaDTO> LifeAreas { get; set; } = [];
    }
}
