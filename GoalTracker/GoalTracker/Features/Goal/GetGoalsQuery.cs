using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public record GetGoalsQuery(string UserId) : IRequest<IEnumerable<GoalDTO>>;
}
