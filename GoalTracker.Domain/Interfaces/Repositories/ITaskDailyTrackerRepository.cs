using GoalTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface ITaskDailyTrackerRepository
    {
        /// <summary>
        /// If current task has in table
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<bool> TaskInDaily(int taskId);
        Task UpdateStatusTask(int taskId, TaskStatus newStatus);
        Task AddToList(TaskItem task, DateTime dateTimeToExecute);
        Task Delete(int taskId);
    }
}
