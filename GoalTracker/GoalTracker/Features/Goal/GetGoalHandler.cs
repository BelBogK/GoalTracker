using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public class GetGoalHandler(IGoalRepository repository, AppMapper mapper)
       : IRequestHandler<GetGoalsQuery, IEnumerable<GoalDTO>>
    {
        public async Task<IEnumerable<GoalDTO>> Handle(
            GetGoalsQuery request,
            CancellationToken cancellationToken)
        {
            var goals = await repository.GetAllAsync(request.UserId); 
            return goals.Select(mapper.ToDto);
        }
    }
}
