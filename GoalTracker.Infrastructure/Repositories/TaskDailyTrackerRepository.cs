using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class TaskDailyTrackerRepository : ITaskDailyTrackerRepository
    {
        public Task AddToList(TaskItem task, DateTime dateTimeToExecute)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TaskInDaily(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusTask(int taskId, TaskStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
