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
        /// <summary>
        /// Если задачу хорошо сделал, то к примеру 10 очков, больше нельзя
        /// </summary>
        public int MaxPointForTask { get; set; } = 0;
        /// <summary>
        /// Когда закончил работу, то оцениваешь что сделал ее на ...Но не больше чем максПоинті. Очень важно понимать что 10 это возможно, и не надо 
        /// думать что это могло быть еще лучше. 10 это 10 из 10 хорошо и доволен собой.
        /// </summary>
        public int RealPointForTask { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
