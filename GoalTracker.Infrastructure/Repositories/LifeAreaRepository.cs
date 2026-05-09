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

            // 1. Находим все проекты, содержащие эти задачи
            var projects = await context.Projects
                .Include(p => p.TaskItems.Where(t => taskIds.Contains(t.Id)))
                .Where(p => p.TaskItems.Any(t => taskIds.Contains(t.Id)))
                .ToListAsync();

            var projectIds = projects.Select(p => p.Id).ToList();

            // 2. Загружаем ВСЕ активные сценарии и их связи (ChildRelations)
            // Благодаря Fix-up, EF сам построит дерево любой глубины в памяти
            var allScenarios = await context.GoalScenarios
                .Include(s => s.ChildRelations)
                .Include(s => s.Projects) // Чтобы связать сценарии с проектами
                .Where(s => s.IsActive)
                .ToListAsync();

            // 3. Загружаем цели, привязанные к этим проектам или сценариям
            var scenarioIds = allScenarios.Select(s => s.Id).ToList();
            var goals = await context.Goals
                .Include(g => g.Projects)
                .Include(g => g.Scenarios)
                .Where(g => g.Projects.Any(p => projectIds.Contains(p.Id)) ||
                            g.Scenarios.Any(s => scenarioIds.Contains(s.Id)))
                .ToListAsync();

            var goalIds = goals.Select(g => g.Id).ToList();

            // 4. Загружаем LifeAreas для этих целей
            var lifeAreas = await context.LifeAreas
                .Include(la => la.Goals)
                .Where(la => la.Goals.Any(g => goalIds.Contains(g.Id)))
                .ToListAsync();

            // На этом этапе EF Core уже связал объекты: 
            // LifeArea.Goals -> Goal.Scenarios -> Scenario.ChildRelations -> ...

            // 5. Возвращаем только те LifeAreas, которые действительно ведут к искомым задачам
            // (Фильтрация "сверху вниз", чтобы отсечь лишние ветки, если это необходимо)
            return lifeAreas;
        }

        public Task<LifeArea> UpdateAsync(LifeArea entity)
        {
            throw new NotImplementedException();
        }
    }
}
