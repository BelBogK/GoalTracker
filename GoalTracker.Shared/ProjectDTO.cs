using GoalTracker.Shared.Enums;
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
        public CurrentStatus CurrentStatus { get; set; }
        public ProjectType GoalType { get; set; }
        public Priority Priority { get; set; }
    }
}
