using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class TaskItemRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ITaskDailyTrackerRepository taskDailyTrackerRepository) : ITaskItemRepository
    {
        public async Task<TaskItem> CreateAsync(TaskItem entity)
        {
            using var context =await contextFactory.CreateDbContextAsync();
            entity.Created = DateTime.Now;
            context.TaskItems.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TaskItem> CreateAsync(TaskItem entity, int projectId)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var project = context.Projects.First(x => x.Id == projectId);
            entity.Projects.Add(project);
            entity.Created = DateTime.Now;
            context.TaskItems.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var removeItem=await context
                .TaskItems
                .Include(X=>X.Projects)
                .FirstOrDefaultAsync(x=>x.Id==id);

            if (removeItem == null)
                return;
            removeItem.Projects.Clear();
            context.TaskItems.Remove(removeItem);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync(string userId)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.TaskItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TaskItem>> GetTasksForProject(int projectId, string userId)
        {
            using var context =await contextFactory.CreateDbContextAsync();
            return await context.TaskItems
                .Where(t => t.Projects.Any(p => p.Id == projectId) && t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskItem> UpdateAsync(TaskItem entity)
        {
            var needUpdateList = await taskDailyTrackerRepository.TaskInDaily(entity.Id);
            using var context = await contextFactory.CreateDbContextAsync();
            var entityDb = await context.TaskItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entityDb != null)
            {
                if (entityDb.CurrentStatus != entity.CurrentStatus)
                {
                    var statutesWithUpdateDate = new[] { CurrentStatus.Achieved, CurrentStatus.Completed, CurrentStatus.Cancelled, CurrentStatus.Failed };
                    if (statutesWithUpdateDate.Contains(entity.CurrentStatus))
                    {
                        entity.StartAt = DateTime.UtcNow;
                    }
                }
            }
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            if (needUpdateList)
            {
                //await taskDailyTrackerRepository.UpdateStatusTask(entity.Id, entity.CurrentStatus);
            }
            return entity;
        }
    }
}
