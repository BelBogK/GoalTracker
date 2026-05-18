using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class DayCommentDTO
    {
        public DateOnly Date { get; set; }
        public string Comment { get; set; }
        public DayScore DayScore { get; set; }
    }
}
