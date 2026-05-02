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

        public Task<LifeArea?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LifeArea>> GetDefaultLifeAreas()
        {
            using var context = contextFactory.CreateDbContext();
            return await context.LifeAreas.Where(l => l.UserId == Guid.Empty.ToString()).ToListAsync();
        }

        public Task<LifeArea> UpdateAsync(LifeArea entity)
        {
            throw new NotImplementedException();
        }
    }
}
