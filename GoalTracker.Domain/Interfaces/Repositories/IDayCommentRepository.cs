using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface IDayCommentRepository
    {
        Task<List<DayComment>> GetCommentsForPeriodAsync(string userId, DateOnly startDate, DateOnly endDate);
        Task SaveCommentAsync(string userId, DayCommentDTO commentDto);
    }
}
