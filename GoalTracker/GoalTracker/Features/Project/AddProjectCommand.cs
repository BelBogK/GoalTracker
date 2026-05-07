using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public record AddProjectCommand(
      string UserId,
      ProjectDTO project,
      int? goalId) : IRequest<ProjectDTO>;

    public record AddProjectToScenCommand(
      string UserId,
      ProjectDTO project,
      int? scenId) : IRequest<ProjectDTO>;

}
