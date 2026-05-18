using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Repositories
{
    public class DayCommentRepository(IDbContextFactory<ApplicationDbContext> contextFactor) : IDayCommentRepository
    { 
        public async Task<List<DayComment>> GetCommentsForPeriodAsync(string userId, DateOnly startDate, DateOnly endDate)
        {
            var context = await contextFactor.CreateDbContextAsync();
            return await context.DayComments
               .Where(c => c.UserId == userId && c.Date >= startDate && c.Date <= endDate)
               .ToListAsync();
        }

        public async Task SaveCommentAsync(string userId, DayCommentDTO commentDto)
        {
            var context = await contextFactor.CreateDbContextAsync();
            var existingComment = await context.DayComments
              .FirstOrDefaultAsync(c => c.UserId == userId && c.Date == commentDto.Date);

            if (existingComment != null)
            {
                existingComment.Comment = commentDto.Comment;
                existingComment.DayScore = commentDto.DayScore;
            }
            else
            {
                var newComment = new DayComment
                {
                    UserId = userId,
                    Date = commentDto.Date,
                    Comment = commentDto.Comment,
                    DayScore = commentDto.DayScore
                };
                await context.DayComments.AddAsync(newComment);
            }

            await context.SaveChangesAsync();
        }
    }
}
