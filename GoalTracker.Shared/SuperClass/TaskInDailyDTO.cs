using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared.SuperClass
{
    public class TaskInDailyDTO
    {
        public TaskItemDTO Task { get; set; }
        public List<ProjectDTO> Projects { get; set; }
        public List<GoalScenarioDTO> ScenarioDTOs { get; set; }
        public List<GoalDTO> Goals { get; set; }
        public List<LifeAreaDTO> LifeAreas { get; set; }
    }
}
