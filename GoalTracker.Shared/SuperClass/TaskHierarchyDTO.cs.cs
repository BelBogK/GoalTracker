using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared.SuperClass
{ 
    public class TaskHierarchyLifeAreaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<TaskHierarchyGoalDTO> Goals { get; set; } = [];
    }

    public class TaskHierarchyGoalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public GoalType GoalType { get; set; }
        public Priority Priority { get; set; }
        public List<TaskHierarchyScenarioDTO> Scenarios { get; set; } = [];
        public List<TaskHierarchyProjectDTO> Projects { get; set; } = [];
    }

    public class TaskHierarchyScenarioDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsOprimisticScenarios { get; set; }
        public List<TaskHierarchyScenarioDTO> Children { get; set; } = [];
        public List<TaskHierarchyProjectDTO> Projects { get; set; } = [];
    }

    public class TaskHierarchyProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public Priority Priority { get; set; }
        public int Points { get; set; }
        public int AchievePoints { get; set; }
        public List<TaskItemDTO> Tasks { get; set; } = [];
    }
}
