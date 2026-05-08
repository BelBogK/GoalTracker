using GoalTracker.Domain.Entities.Base;
using GoalTracker.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities
{
    /// <summary>
    /// Нельзя удалить или отказаться от задачи которая сюда попала. Только поменяв статус самой задачи.
    /// А если отменена, тогда пусть пишет объяснение, может на потом отложить 2 раза. Потом начинать заебывать клиента
    /// </summary>
    public class TaskDailyTracker: BaseWithUser
    {
        public int Id { get; set; }
        public DateTime TodayIs { get; set;  }
        public int TaskItemId { get; set; }
        public virtual TaskItem Task { get; set; }
        public CurrentStatus StatusInBegining { get; set; }
    }
}
