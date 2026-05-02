using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public record AddProjectCommand(
      string UserId,
      ProjectDTO project) : IRequest<ProjectDTO>;

    public record AddGoalProjectCommand(
  string UserId,
  int goalId,
  ProjectDTO project) : IRequest<ProjectDTO>;
}
