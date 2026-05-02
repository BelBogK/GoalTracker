using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Project
{
    public record GetProjectQuery(string UserId, int? GoalId = null) : IRequest<IEnumerable<ProjectDTO>>;
}
