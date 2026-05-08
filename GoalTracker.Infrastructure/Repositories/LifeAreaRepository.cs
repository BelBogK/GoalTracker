using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class LifeAreaRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : ILifeAreaRepository
    {
        public Task<LifeArea> CreateAsync(LifeArea entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LifeArea>> CreateRangeAsync(IEnumerable<LifeArea> entities)
        {
            var list = entities.ToList();
            using var context = contextFactory.CreateDbContext();
            context.LifeAreas.AddRange(list);
            await context.SaveChangesAsync();
            return list;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LifeArea>> GetAllAsync(string userId)
        {
            using var context = contextFactory.CreateDbContext();
            var result= await context.LifeAreas.Where(l => l.UserId == userId).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LifeArea?> GetByIdAsync(int id)
        {
            using var context=await contextFactory.CreateDbContextAsync();
            return await context.LifeAreas.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<LifeArea>> GetDefaultLifeAreas()
        {
            using var context = contextFactory.CreateDbContext();
            return await context.LifeAreas.Where(l => l.UserId == Guid.Empty.ToString()).ToListAsync();
        }

        public async Task<IEnumerable<LifeArea>> GetWithAllPathToTask(int taskId, string userId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            // Находим проекты которые содержат эту задачу
            var projectIds = await context.Projects
                .Where(p => p.TaskItems.Any(t => t.Id == taskId))
                .Select(p => p.Id)
                .ToListAsync();

            // Находим сценарии которые содержат эти проекты (вся иерархия вверх)
            var scenarioIds = await context.GoalScenarios
                .Where(s => s.Projects.Any(p => projectIds.Contains(p.Id)))
                .Select(s => s.Id)
                .ToListAsync();

            // Находим цели через проекты или сценарии
            var goalIds = await context.Goals
                .Where(g =>
                    g.Projects.Any(p => projectIds.Contains(p.Id)) ||
                    g.Scenarios.Any(s => scenarioIds.Contains(s.Id)))
                .Select(g => g.Id)
                .ToListAsync();

            // Загружаем LifeAreas с полной иерархией
            return await context.LifeAreas
     .Where(la => la.Goals.Any(g => goalIds.Contains(g.Id)))
     .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
         .ThenInclude(g => g.Scenarios)
             .ThenInclude(s => s.ChildRelations)
                 .ThenInclude(cr => cr.Child)
                     .ThenInclude(c => c.Projects)
     .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
         .ThenInclude(g => g.Scenarios)
             .ThenInclude(s => s.Projects)
     .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
         .ThenInclude(g => g.Projects)
     .ToListAsync();
        }

        public async Task<IEnumerable<LifeArea>> GetLifeAreasByTaskIdsAsync(IEnumerable<int> taskItemIds)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var taskIds = taskItemIds.Distinct().ToList();

            // Projects containing tasks
            var projectIds = await context.Projects
                .Where(p => p.TaskItems.Any(t => taskIds.Contains(t.Id)))
                .Select(p => p.Id)
                .Distinct()
                .ToListAsync();

            // Scenarios containing projects
            var scenarioIds = await context.GoalScenarios
                .Where(s => s.Projects.Any(p => projectIds.Contains(p.Id)))
                .Select(s => s.Id)
                .Distinct()
                .ToListAsync();

            // Goals connected through projects or scenarios
            var goalIds = await context.Goals
                .Where(g =>
                    g.Projects.Any(p => projectIds.Contains(p.Id)) ||
                    g.Scenarios.Any(s => scenarioIds.Contains(s.Id)))
                .Select(g => g.Id)
                .Distinct()
                .ToListAsync();

            return await context.LifeAreas
                .Where(la => la.Goals.Any(g => goalIds.Contains(g.Id)))

                // Goals -> Direct Projects -> Tasks
                .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
                    .ThenInclude(g => g.Projects
                        .Where(p => projectIds.Contains(p.Id)))
                            .ThenInclude(p => p.TaskItems
                                .Where(t => taskIds.Contains(t.Id)))

                // Goals -> Scenarios
                .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
                    .ThenInclude(g => g.Scenarios)

                // Goals -> Scenarios -> Projects -> Tasks
                .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
                    .ThenInclude(g => g.Scenarios)
                        .ThenInclude(s => s.Projects
                            .Where(p => projectIds.Contains(p.Id)))
                                .ThenInclude(p => p.TaskItems
                                    .Where(t => taskIds.Contains(t.Id)))

                // Goals -> Scenarios -> ChildRelations -> Child
                .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
                    .ThenInclude(g => g.Scenarios)
                        .ThenInclude(s => s.ChildRelations)
                            .ThenInclude(cr => cr.Child)

                // Goals -> Scenarios -> ChildRelations -> Child -> Projects -> Tasks
                .Include(la => la.Goals.Where(g => goalIds.Contains(g.Id)))
                    .ThenInclude(g => g.Scenarios)
                        .ThenInclude(s => s.ChildRelations)
                            .ThenInclude(cr => cr.Child)
                                .ThenInclude(c => c.Projects
                                    .Where(p => projectIds.Contains(p.Id)))
                                        .ThenInclude(p => p.TaskItems
                                            .Where(t => taskIds.Contains(t.Id)))

                .AsSplitQuery()
                .ToListAsync();
        }

        public Task<LifeArea> UpdateAsync(LifeArea entity)
        {
            throw new NotImplementedException();
        }
    }
}
