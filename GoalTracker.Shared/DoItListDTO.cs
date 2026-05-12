using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class DoItListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public List<DoItListItemDTO> Items { get; set; } = [];
    }
}
