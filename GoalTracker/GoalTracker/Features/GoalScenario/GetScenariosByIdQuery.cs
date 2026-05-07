using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.GoalScenario
{
    public record GetScenariosByIdQuery(string UserId, int scenId)
      : IRequest<GoalScenarioDTO>;

    public class GetScenariosByIdHandler(IGoalScenarioRepository repository, AppMapper mapper)
        : IRequestHandler<GetScenariosByIdQuery, GoalScenarioDTO>
    {
        public async Task<GoalScenarioDTO> Handle(
            GetScenariosByIdQuery request, CancellationToken ct)
        {
            var scenario = await repository.GetByIdWithChildrenAsync(request.scenId);
            var result= new GoalScenarioDTO
            {
                Description = scenario.Description,
                Id = scenario.Id,
                IsActive = scenario.IsActive,
                Name = scenario.Name,
                ChildRelations = [],
                Projects = []
            }; 
            foreach(var sc in scenario.ChildRelations)
            {
                if (sc.Child == null)
                    continue;
                result.ChildRelations.Add(new GoalScenarioDTO()
                {
                    Id = sc.Child.Id,
                    Description = sc.Child.Description,
                    Name = sc.Child.Name,
                    IsActive = sc.Child.IsActive
                });
            }
            return result;
        }
    }
   
}
