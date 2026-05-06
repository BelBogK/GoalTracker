using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.GoalScenario
{ 
    public record UpdateGoalScenarioCommand(
        string UserId,
        GoalScenarioDTO Scenario) : IRequest<GoalScenarioDTO>;

    public class UpdateGoalScenarioHandler(IGoalScenarioRepository repository, AppMapper mapper)
        : IRequestHandler<UpdateGoalScenarioCommand, GoalScenarioDTO>
    {
        public async Task<GoalScenarioDTO> Handle(
            UpdateGoalScenarioCommand request, CancellationToken ct)
        {
            var entity = await repository.GetByIdAsync(request.Scenario.Id);
            if (entity is null) throw new Exception($"Scenario {request.Scenario.Id} not found");

            entity.Name = request.Scenario.Name;
            entity.Description = request.Scenario.Description;
            entity.IsActive = request.Scenario.IsActive;

            var updated = await repository.UpdateAsync(entity);
            return mapper.ToDto(updated);
        }
    }
}
