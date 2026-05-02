using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.LifeArea
{
    public record AddLifeAreasCommand(
     string UserId,
     IEnumerable<LifeAreaDTO> Items) : IRequest<IEnumerable<LifeAreaDTO>>;
}