using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Infrastructure.Repositories
{
    public class ProjectRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IProjectRepository
    {
        public Task<Project> CreateAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> CreateAsync(Project entity, int goalId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var goal = await context.Goals.FindAsync(goalId);
            if (goal != null)
            {
                entity.Goals.Add(goal);
            }

            context.Projects.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllAsync(string userId)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Projects.Where(p => p.UserId == userId).ToListAsync();
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
