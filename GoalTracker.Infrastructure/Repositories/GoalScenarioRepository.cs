using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    // Infrastructure/Repositories/GoalScenarioRepository.cs
    public class GoalScenarioRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IGoalScenarioRepository
    {
        public async Task<IEnumerable<GoalScenario>> GetAllAsync(string userId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.GoalScenarios
                .Where(s => s.UserId == userId)
                .Include(s => s.ChildRelations)
                .Include(s => s.Projects)
                .ToListAsync();
        }

        public async Task<GoalScenario?> GetByIdAsync(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.GoalScenarios.FindAsync(id);
        }

        public async Task<GoalScenario?> GetByIdWithChildrenAsync(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.GoalScenarios
                .Include(s => s.ChildRelations)
                    .ThenInclude(c => c.Child.Projects)
                .Include(s => s.Projects)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<GoalScenario>> GetByGoalIdAsync(int goalId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.GoalScenarios
                .Where(s => s.Goals.Any(g => g.Id == goalId))
                .Include(s => s.ChildRelations)
                    .ThenInclude(c => c.Child.Projects)
                .Include(s => s.Projects)
                .ToListAsync();
        }

        public async Task<GoalScenario> CreateAsync(GoalScenario entity)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.GoalScenarios.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<GoalScenario>> CreateRangeAsync(IEnumerable<GoalScenario> entities)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var list = entities.ToList();
            await context.GoalScenarios.AddRangeAsync(list);
            await context.SaveChangesAsync();
            return list;
        }

        public async Task<GoalScenario> UpdateAsync(GoalScenario entity)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.GoalScenarios.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var entity = await context.GoalScenarios.FindAsync(id);
            if (entity is not null)
            {
                context.GoalScenarios.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<GoalScenario> CreateAsync(GoalScenario scenario, int goalId)
        {
            await using var context= await contextFactory.CreateDbContextAsync();
            var sc = context.GoalScenarios.Add(scenario);
            var goal=context.Goals.First(x => x.Id == goalId);
            goal.Scenarios.Add(sc.Entity);
            await context.SaveChangesAsync();
            return sc.Entity;
        }
    }
}
