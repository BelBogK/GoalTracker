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
            var result= await GetLifeAreasByTaskIdsAsync([taskId]);
            return result;
        }

        public async Task<IEnumerable<LifeArea>> GetLifeAreasByTaskIdsAsync(IEnumerable<int> taskItemIds)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var taskIds = taskItemIds.Distinct().ToList();

            // 1. All active scenarios with relations and projects — build tree in memory
            var allScenarios = await context.GoalScenarios
                .Where(s => s.IsActive)
                .Include(s => s.ChildRelations).ThenInclude(r => r.Child)
                .Include(s => s.Projects).ThenInclude(p => p.TaskItems.Where(t => taskIds.Contains(t.Id)))
                .AsSplitQuery()
                .ToListAsync();

            // 2. LifeAreas with goals, direct projects and scenario links
            var lifeAreas = await context.LifeAreas
                .Include(la => la.Goals)
                    .ThenInclude(g => g.Projects)
                        .ThenInclude(p => p.TaskItems.Where(t => taskIds.Contains(t.Id)))
                .Include(la => la.Goals)
                    .ThenInclude(g => g.Scenarios)  // just the link Goal→Scenario (no deep include)
                .AsSplitQuery()
                .ToListAsync();

            // 3. EF fix-up wires GoalScenario.Projects and ChildRelations automatically
            // because allScenarios are tracked in the same context instance

            return lifeAreas;
        }

        public async Task<Dictionary<string, int>> GetLifeAreasWithPoints(
     string userId, DateTime startTime, DateTime endTime)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            //var lifeAreas = await context.LifeAreas
            //    .Where(la => la.UserId == userId)
            //    .Select(la => new
            //    {
            //        la.Name,
            //        GoalPoints = la.Goals
            //            .Where(g => g.FinishedAt.HasValue &&
            //                        g.FinishedAt >= startTime &&
            //                        g.FinishedAt <= endTime)
            //            .Sum(g => g.PointsForCompletedGoal),

            //        ProjectPoints = la.Goals
            //            .SelectMany(g => g.Projects)
            //            .Where(p => p.FinishedAt.HasValue &&
            //                        p.FinishedAt >= startTime &&
            //                        p.FinishedAt <= endTime)
            //            .Sum(p => p.PointsForCompletedProject),

            //        TaskPoints = la.Goals
            //            .SelectMany(g => g.Projects)
            //            .SelectMany(p => p.TaskItems)
            //            .Where(t => t.FinishedAt.HasValue &&
            //                        t.FinishedAt >= startTime &&
            //                        t.FinishedAt <= endTime)
            //            .Sum(t => t.RealPointForTask)
            //    })
            //    .ToListAsync();

            var today = DateTime.Today;

            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            var startOfWeek = today.AddDays(-diff);
            var endOfWeek = startOfWeek.AddDays(6);
            var lifeArea = await context.LifeAreas
                   .Where(la => la.UserId == userId)
                   .Select(x => new
                   {
                       Name = x.Name,
                       TaskCount = x.Goals
                       .SelectMany(x => x.Projects)
                       .SelectMany(x => x.TaskItems)
                       //.Where(x => x.CurrentStatus == Shared.Enums.CurrentStatus.Completed)
                       .Where(x => x.StartAt >= startOfWeek && x.StartAt <= endOfWeek && x.CurrentStatus > Shared.Enums.CurrentStatus.OnHold)
                       .Count()
                   }).ToListAsync();

            return lifeArea.ToDictionary(
                la => la.Name,
                la => la.TaskCount);

            //return lifeAreas.ToDictionary(
            //    la => la.Name,
            //    la => la.GoalPoints + la.ProjectPoints + la.TaskPoints);
        }

        public async Task<Dictionary<string, int>> GetLifeAreasWithPotentialPoints(
     string userId, DateTime startTime, DateTime endTime)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            try
            {
                //var lifeAreas = await context.LifeAreas
                //    .Where(la => la.UserId == userId)
                //    .Select(la => new
                //    {
                //        la.Name,
                //        GoalPoints = la.Goals
                //            .Where(g => g.FinishedAt.HasValue &&
                //                        g.FinishedAt >= startTime &&
                //                        g.FinishedAt <= endTime)
                //            .Sum(g => g.PointsForCompletedGoal),

                //        ProjectPoints = la.Goals
                //            .SelectMany(g => g.Projects)
                //            .Where(p => p.FinishedAt.HasValue &&
                //                        p.FinishedAt >= startTime &&
                //                        p.FinishedAt <= endTime)
                //            .Sum(p => p.PointsForCompletedProject),

                //        TaskPoints = la.Goals
                //            .SelectMany(g => g.Projects)
                //            .SelectMany(p => p.TaskItems)
                //            .Where(t => t.FinishedAt.HasValue &&
                //                        t.FinishedAt >= startTime &&
                //                        t.FinishedAt <= endTime)
                //            .Sum(t => t.RealPointForTask)
                //    })
                //    .ToListAsync();

                var today = DateTime.Today;

                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                var startOfWeek = today.AddDays(-diff);
                var endOfWeek = startOfWeek.AddDays(6); 

                var lifeArea = await context.LifeAreas
                    .Where(la => la.UserId == userId)
                    .Select(x => new
                    {
                        Name = x.Name,
                        TaskCount = x.Goals
                        .SelectMany(x => x.Projects)
                        .SelectMany(x => x.TaskItems)
                        //.Where(x => x.CurrentStatus == Shared.Enums.CurrentStatus.InProgress)
                        .Where(x => (x.StartAt < startOfWeek && x.CurrentStatus < Shared.Enums.CurrentStatus.OnHold) || (x.StartAt >= startOfWeek && x.StartAt <= endOfWeek))
                        .Count()
                    }).ToListAsync();

                return lifeArea.ToDictionary(
                    la => la.Name,
                    la => la.TaskCount);
            }
            catch (Exception ex) {
                int a = 0;
            }
            return null;
        }

        public Task<LifeArea> UpdateAsync(LifeArea entity)
        {
            throw new NotImplementedException();
        }
    }
}
