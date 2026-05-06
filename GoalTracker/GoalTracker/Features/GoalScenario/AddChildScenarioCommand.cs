using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Features.GoalScenario
{
    public record AddChildScenarioCommand(
    string UserId,
    int ParentId,
    GoalScenarioDTO Child) : IRequest<GoalScenarioDTO>;

    public class AddChildScenarioHandler(
        IGoalScenarioRepository repository,
        IDbContextFactory<ApplicationDbContext> contextFactory,
        AppMapper mapper)
        : IRequestHandler<AddChildScenarioCommand, GoalScenarioDTO>
    {
        public async Task<GoalScenarioDTO> Handle(
            AddChildScenarioCommand request, CancellationToken ct)
        {
            // Create child scenario
            var child = new GoalTracker.Domain.Entities.GoalScenario
            {
                Name = request.Child.Name,
                Description = request.Child.Description,
                IsActive = request.Child.IsActive,
                UserId = request.UserId
            };
            var created = await repository.CreateAsync(child);

            // Create relation
            await using var context = await contextFactory.CreateDbContextAsync();
            context.GoalScenarioRelations.Add(new GoalScenarioRelation
            {
                ParentId = request.ParentId,
                ChildId = created.Id
            });
            await context.SaveChangesAsync();

            return mapper.ToDto(created);
        }
    }
}
