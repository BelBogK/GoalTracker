using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Shared
{
    public class TaskItemDTO
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
        public DateTime Created { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public ICollection<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
    }
}
