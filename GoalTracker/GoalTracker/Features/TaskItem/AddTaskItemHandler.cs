using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.TaskItem
{
    public class AddTaskItemHandler(ITaskItemRepository repository, AppMapper mapper)
     : IRequestHandler<AddTaskItemCommand, TaskItemDTO>
    {
        public async Task<TaskItemDTO> Handle(AddTaskItemCommand request, CancellationToken cancellationToken)
        {
            var item = mapper.ToEntity(request.task);
            if (item == null)
            {
                return null;
            }
            item.UserId = request.UserId;
            GoalTracker.Domain.Entities.TaskItem result;
            if(request.projectId.HasValue)
            {
                result = await repository.CreateAsync(item, request.projectId.Value);
            }
            else
            {
                result = await repository.CreateAsync(item);
            }
            
            return mapper.ToDto(result);
        }
    }
}
