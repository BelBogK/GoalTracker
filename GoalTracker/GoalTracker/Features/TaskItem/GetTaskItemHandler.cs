using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Extensions;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using GoalTracker.Shared.SuperClass;
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
    public class GetTaskItemByIdHandler(ITaskItemRepository repository, AppMapper mapper)
       : IRequestHandler<GetTaskByIdQuery, TaskItemDTO>
    { 
        public async Task<TaskItemDTO> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var result =await repository.GetByIdAsync(request.taskId);
            return result.ToDto();
        }
    }

    public class UpdateTaskHandler(ITaskItemRepository repository, AppMapper mapper)
      : IRequestHandler<UpdateTaskQuery, TaskItemDTO>
    {
        public async Task<TaskItemDTO> Handle(UpdateTaskQuery request, CancellationToken cancellationToken)
        {
            var entity = request.task.ToEntity();
            entity.UserId = request.userId;
            var result = await repository.UpdateAsync(entity);
            return result.ToDto();
        }
    }

    public class GetTaskHierarchyLifeAreaHandler(ITaskDailyTrackerRepository repository, AppMapper mapper)
      : IRequestHandler<GetDailyTrackerQuery, IEnumerable<TaskHierarchyLifeAreaDTO>>
    {
        public async Task<IEnumerable<TaskHierarchyLifeAreaDTO>> Handle(GetDailyTrackerQuery request, CancellationToken cancellationToken)
        {
            var result=await repository.TrackedTask(request.UserId, request.start, request.end);
            return result.Select(x=>x.ToTaskHierarchyDto());
        }
    }

    public class GetTasksByRangeHandler(ITaskDailyTrackerRepository repository, AppMapper mapper)
    : IRequestHandler<GetTasksByRangeQuery, IEnumerable<TaskItemDTO>>
    { 
        public async Task<IEnumerable<TaskItemDTO>> Handle(GetTasksByRangeQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetTasksByRange(request.UserId, request.start, request.end);
            return result.Select(x => x.ToDto());
        }
    }

    public class GetNonTrackedQueryHandler(ITaskDailyTrackerRepository repository, AppMapper mapper)
      : IRequestHandler<GetNonTrackedQuery, IEnumerable<TaskHierarchyLifeAreaDTO>>
    {
        public async Task<IEnumerable<TaskHierarchyLifeAreaDTO>> Handle(GetNonTrackedQuery request, CancellationToken cancellationToken)
        {
            var resultItems= await repository.NonTrackedTask(request.UserId);
            var result= resultItems.Select(x => x.ToTaskHierarchyDto());
            return result;
        }
    }

    public class AddTrackedQueryHandler(ITaskDailyTrackerRepository repositoryr)
      : IRequestHandler<AddTrackedQuery, TaskHierarchyLifeAreaDTO>
    {
        public async Task<TaskHierarchyLifeAreaDTO> Handle(AddTrackedQuery request, CancellationToken cancellationToken)
        {
            var result= await repositoryr.AddToTracked(request.UserId, request.taskId, request.StartTime);
            return result.First().ToTaskHierarchyDto(); 
        }
    }

    public class RemoveFromTrackerAsyncHandler(ITaskDailyTrackerRepository repository)
     : IRequestHandler<RemoveFromTrackerQuery>
    {
        public async Task Handle(RemoveFromTrackerQuery request, CancellationToken cancellationToken)
        {
            await repository.RemoveTaskFromTrack(request.UserId, request.taskID);
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
