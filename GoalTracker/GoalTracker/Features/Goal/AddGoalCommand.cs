using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public record AddGoalCommand(
      string UserId,
      GoalDTO Goal) : IRequest<GoalDTO>;
    public record UpdateGoalCommand(
      string UserId,
      GoalDTO Goal) : IRequest<GoalDTO>;
}
