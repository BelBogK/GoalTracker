using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoalTracker.Domain.Entities
{ 
    public class DayComment:BaseWithUser
    { 
        public DateOnly Date { get; set; }  
        public string Comment { get; set; }
        public DayScore DayScore { get; set; }
    }
}
