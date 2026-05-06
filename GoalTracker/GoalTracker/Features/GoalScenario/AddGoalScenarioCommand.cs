using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.GoalScenario
{
    public record AddGoalScenarioCommand(
     string UserId,
     int GoalId,
     GoalScenarioDTO Scenario) : IRequest<GoalScenarioDTO>;

    public class AddGoalScenarioHandler(IGoalScenarioRepository repository, AppMapper mapper)
        : IRequestHandler<AddGoalScenarioCommand, GoalScenarioDTO>
    {
        public async Task<GoalScenarioDTO> Handle(
            AddGoalScenarioCommand request, CancellationToken ct)
        {
            var entity = new GoalTracker.Domain.Entities.GoalScenario
            {
                Name = request.Scenario.Name,
                Description = request.Scenario.Description,
                IsActive = request.Scenario.IsActive,
                UserId = request.UserId,
                Created= DateTime.UtcNow,
                
            };
            var created = await repository.CreateAsync(entity, request.GoalId);
            return mapper.ToDto(created);
        }
    }
}
