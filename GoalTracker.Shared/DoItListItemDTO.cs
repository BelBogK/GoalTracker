using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class DoItListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? Link { get; set; }
        public bool IsFinished { get; set; }
        public int DoItListId { get; set; }
    }
}
