using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class TaskItemRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : ITaskItemRepository
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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync(string userId)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.TaskItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskItem>> GetTasksForProject(int projectId, string userId)
        {
            using var context =await contextFactory.CreateDbContextAsync();
            return await context.TaskItems
                .Where(t => t.Projects.Any(p => p.Id == projectId) && t.UserId == userId)
                .ToListAsync();
        }

        public Task<TaskItem> UpdateAsync(TaskItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
