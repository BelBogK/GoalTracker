using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared.Path
{
    public class PathNodeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PathEntityType Type { get; set; }
    }
    public class EntityPathDto
    {
        public List<PathNodeDto> Nodes { get; set; } = [];
    }
}
