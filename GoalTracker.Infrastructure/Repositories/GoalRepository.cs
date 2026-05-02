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
            return await context.Goals.Where(g => g.UserId == userId).ToListAsync();
        }

        public Task<Goal> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Goal> UpdateAsync(Goal entity)
        {
            throw new NotImplementedException();
        }
    }
}
