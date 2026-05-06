using GoalTracker.Domain.Interfaces.Repositories;
using MediatR;

namespace GoalTracker.Features.GoalScenario
{
    public record DeleteGoalScenarioCommand(string UserId, int ScenarioId) : IRequest;

    public class DeleteGoalScenarioHandler(IGoalScenarioRepository repository)
        : IRequestHandler<DeleteGoalScenarioCommand>
    {
        public async Task Handle(DeleteGoalScenarioCommand request, CancellationToken ct)
        {
            await repository.DeleteAsync(request.ScenarioId);
        }
    }
}
