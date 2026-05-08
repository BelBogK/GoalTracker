using GoalTracker.Domain.Entities;
using GoalTracker.Shared.SuperClass;
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
        Task<IEnumerable<LifeArea>>TrackedTask(string userId, DateTime from, DateTime to);
        Task<IEnumerable<LifeArea>>NonTrackedTask(string userId);
        /// <summary>
        /// Because for 1 task can be more then 1 lifeArea
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<IEnumerable<LifeArea>> AddToTracked(string userId, int taskId, DateTime dateTime);
        Task RemoveTaskFromTrack(string userId, int taskId);

        Task UpdateStatusTask(int taskId, TaskStatus newStatus);
        Task AddToList(TaskItem task, DateTime dateTimeToExecute);
        Task Delete(int taskId);
    }
}
