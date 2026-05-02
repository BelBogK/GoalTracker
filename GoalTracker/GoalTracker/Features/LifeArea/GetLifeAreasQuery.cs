using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.LifeArea
{
    public record GetLifeAreasQuery(string UserId) : IRequest<IEnumerable<LifeAreaDTO>>;
}
