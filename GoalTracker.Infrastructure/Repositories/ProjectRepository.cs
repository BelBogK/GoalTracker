using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class ProjectRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IProjectRepository
    {
        public Task<Project> CreateAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetAllAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetByGoalAsync(string userId, int goalId)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Projects.Where(p => p.UserId == userId && p.Goals.Any(g=>g.Id == goalId)).ToListAsync();
        }

        public Task<Project?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> UpdateAsync(Project entity)
        {
            throw new NotImplementedException();
        }
    }
}
