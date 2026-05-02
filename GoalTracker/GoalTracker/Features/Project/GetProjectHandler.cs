using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Project
{
    public class GetProjectHandler(IProjectRepository repository, AppMapper mapper)
       : IRequestHandler<GetProjectQuery, IEnumerable<ProjectDTO>>
    {
        public async Task<IEnumerable<ProjectDTO>> Handle(
            GetProjectQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<GoalTracker.Domain.Entities.Project> projects;
            if(request.GoalId.HasValue)
            {
                projects = await repository.GetByGoalAsync(request.UserId, request.GoalId.Value);
            }
            else
            {
                projects = await repository.GetAllAsync(request.UserId);
            }
            return projects.Select(mapper.ToDto);
        }
    }
}
