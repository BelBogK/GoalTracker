using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    public class TaskItem : BaseWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// Как повлияло
        /// </summary>
        public string? Effort { get; set; }
        /// <summary>
        /// Как сильно в процентах. К примеру заработок увелоичился на 20%, значит 20 процентов. Если заработок уменьшился на 20%, то -20 процентов
        /// </summary>
        public int Procents { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
