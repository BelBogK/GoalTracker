using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared.Enums;
using GoalTracker.Shared.SuperClass;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GoalTracker.Infrastructure.Repositories
{
    public class TaskDailyTrackerRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ILifeAreaRepository lifeAreaRepository) : ITaskDailyTrackerRepository
    { 

        public Task AddToList(TaskItem task, DateTime dateTimeToExecute)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LifeArea>> AddToTracked(string userId, int taskId, DateTime dateTime)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var task = await context.TaskItems.FirstAsync(x=>x.Id==taskId);
            var newItem = new TaskDailyTracker
            {
                UserId = userId,
                Created = DateTime.UtcNow,
                StatusInBegining = task.CurrentStatus,
                Task = task,
                TaskItemId = taskId,
                TodayIs = dateTime
            };
            await context.DailyTrackers.AddAsync(newItem);
            await context.SaveChangesAsync();

            return await lifeAreaRepository.GetWithAllPathToTask(taskId, userId);
        }

        public Task Delete(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LifeArea>> NonTrackedTask(string userId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var trackedTaskIds = context.DailyTrackers.Where(x => x.UserId == userId).Select(t => t.TaskItemId);
            var taskIds=await context.TaskItems.Where(x=>x.UserId==userId && !trackedTaskIds.Contains( x.Id))
                .Select(t=>t.Id).ToListAsync();

            return await lifeAreaRepository.GetLifeAreasByTaskIdsAsync(taskIds);
        }

        public async Task RemoveTaskFromTrack(string userId, int taskId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var taskRemove=context.DailyTrackers.First(x=>x.TaskItemId==taskId);
            context.Remove(taskRemove);
            await context.SaveChangesAsync();

        }

        public async Task<bool> TaskInDaily(int taskId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var result=await context.DailyTrackers.AnyAsync(x=>x.TaskItemId==taskId);
            return result;
        }

        public async Task<IEnumerable<LifeArea>> TrackedTask(string userId, DateTime from, DateTime to)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var trackedTaskIds = context.DailyTrackers.Where(x => x.UserId == userId).Select(t => t.TaskItemId);
             
            return await lifeAreaRepository.GetLifeAreasByTaskIdsAsync(trackedTaskIds);
        }

        public Task UpdateStatusTask(int taskId, CurrentStatus newStatus)
        {
            return null;
        } 
    }
}
