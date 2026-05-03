using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public class AddProjectHandler(IProjectRepository repository, AppMapper mapper)
     : IRequestHandler<AddProjectCommand, ProjectDTO>
    {
        public async Task<ProjectDTO> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var item = mapper.ToEntity(request.project);
            if (item == null)
            {
                return null;
            }
            item.UserId = request.UserId;
            GoalTracker.Domain.Entities.Project result;
            if(request.goalId.HasValue)
            {
                result = await repository.CreateAsync(item, request.goalId.Value);
            }
            else
            {
                result = await repository.CreateAsync(item);
            }
            
            return mapper.ToDto(result);
        }
    }
}
