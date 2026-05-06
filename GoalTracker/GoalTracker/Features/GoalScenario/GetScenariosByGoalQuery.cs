using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.GoalScenario
{
    public record GetScenariosByGoalQuery(string UserId, int GoalId)
       : IRequest<IEnumerable<GoalScenarioDTO>>;

    public class GetScenariosByGoalHandler(IGoalScenarioRepository repository, AppMapper mapper)
        : IRequestHandler<GetScenariosByGoalQuery, IEnumerable<GoalScenarioDTO>>
    {
        public async Task<IEnumerable<GoalScenarioDTO>> Handle(
            GetScenariosByGoalQuery request, CancellationToken ct)
        {
            var scenarios = await repository.GetByGoalIdAsync(request.GoalId);
            return scenarios.Select(mapper.ToDto);
        }
    }
}
