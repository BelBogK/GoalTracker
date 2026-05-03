using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public record AddProjectCommand(
      string UserId,
      ProjectDTO project,
      int? goalId) : IRequest<ProjectDTO>;
     
}
