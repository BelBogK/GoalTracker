using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class LifeAreaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? IdealVision { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
