using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class DoItList : BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DoItListType Type { get; set; } = DoItListType.General;
        public virtual ICollection<DoItListItem> Items { get; set; } = [];
    }
}
