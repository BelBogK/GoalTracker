using GoalTracker.Shared.Enums;
using GoalTracker.Shared.Path;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Result { get; set; }
        public int? ParentProjectId { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public ProjectType GoalType { get; set; }
        public Priority Priority { get; set; }
        public ProjectDTO? ParentProject { get; set; }
        public List<ProjectDTO> NextProjects { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public DateTime Created { get; set; }
        public List<GoalDTO> Goals { get; set; } = new List<GoalDTO>();
        public int Points { get; set; }
        public int AchievePoints { get; }
        public List<EntityPathDto> Path { get; set; }
    }
}
