using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class GoalRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IGoalRepository
    {
        public async Task<Goal> CreateAsync(Goal entity)
        {
            using var context = contextFactory.CreateDbContext();
            var lifeAreas = entity.LifeAreas.Select(x=>x.Id).ToList();
            entity.LifeAreas.Clear();
            foreach(var item in context.LifeAreas.Where(x => lifeAreas.Contains(x.Id)))
            {
                entity.LifeAreas.Add(item);
            }
            
            context.Goals.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Goal>> GetAllAsync(string userId)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Goals.Where(g => g.UserId == userId)
                .Include(x => x.Scenarios)
                .ThenInclude(x => x.ChildRelations)
                .ThenInclude(c=>c.Child)
                .Include(x=>x.LifeAreas)
                .ToListAsync();
        }

        public Task<Goal> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Goal>> GetByLifeAreaAsync(string userId, int lifeAreaId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            return await context.Goals
                .Where(g => g.UserId == userId && g.LifeAreas.Any(l => l.Id == lifeAreaId))
                .Include(g => g.Scenarios)
                    .ThenInclude(s => s.ChildRelations)
                        .ThenInclude(cr => cr.Child)
                .Include(g => g.Scenarios)
                    .ThenInclude(s => s.Projects)
                .ToListAsync();
        }

        public Task<Goal> UpdateAsync(Goal entity)
        {
            throw new NotImplementedException();
        }
    }
}
