using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.TaskItem
{
    public class GetTaskItemHandler(ITaskItemRepository repository, AppMapper mapper)
       : IRequestHandler<GetTaskItemQuery, IEnumerable<TaskItemDTO>>
    {
        public async Task<IEnumerable<TaskItemDTO>> Handle(
            GetTaskItemQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<GoalTracker.Domain.Entities.TaskItem> projects;
            if(request.ProjectId.HasValue)
            {
                projects = await repository.GetTasksForProject(request.ProjectId.Value, request.UserId);
            }
            else
            {
                projects = await repository.GetAllAsync(request.UserId);
            }
            return projects.Select(mapper.ToDto);
        }
    }

    public class DeleteTaskItemHandler(ITaskItemRepository repository, AppMapper mapper)
      : IRequestHandler<DeleteTaskItemQuery, TaskItemDTO>
    { 
        public async Task<TaskItemDTO> Handle(DeleteTaskItemQuery request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.taskID);
            return null;
        }
    }
}
