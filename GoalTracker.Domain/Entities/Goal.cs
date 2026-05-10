using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class Goal: BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }//SMART goal description
        public DateTime? TargetDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Reward { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public GoalType GoalType { get; set; }
        public Priority Priority { get; set; }
        public string? IdealVision { get; set; }
        /// <summary>
        /// Дополнителькые бонусы к проектам которые выполнились и задачам. ТАк же не выполненные проекты и таски сумировать надо которые были в активных сценариях
        /// </summary>
        public int PointsForCompletedGoal { get; set; }
        public DateTime? FinishedAt { get; set; }
        public virtual ICollection<GoalScenario> Scenarios { get; set; } = new List<GoalScenario>();
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<LifeArea> LifeAreas { get; set; }= new List<LifeArea>();
    }
}
