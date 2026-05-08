using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared.SuperClass.Containers
{
    public class LifeAreaContainerDTO
    {
        public LifeAreaDTO LifeArea { get; set; }
        public List<GoalScenarioDTO> Scenario { get; set; }
        public List<ProjectDTO> Project { get; set; }
    }
}
