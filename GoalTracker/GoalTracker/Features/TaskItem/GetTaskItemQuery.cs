using GoalTracker.Shared;
using GoalTracker.Shared.SuperClass;
using MediatR;

namespace GoalTracker.Features.TaskItem
{
    public record GetTaskItemQuery(string UserId, int? ProjectId = null) : IRequest<IEnumerable<TaskItemDTO>>;
    public record GetTaskByIdQuery(string UserId, int taskId) : IRequest<TaskItemDTO>;
    public record UpdateTaskQuery(string userId, int taskId, TaskItemDTO task) : IRequest<TaskItemDTO>;
    public record GetDailyTrackerQuery(string UserId, DateTime start, DateTime end) : IRequest<IEnumerable<TaskHierarchyLifeAreaDTO>>;
    public record GetTasksByRangeQuery(string UserId, DateTime start, DateTime end) : IRequest<IEnumerable<TaskItemDTO>>;
    public record GetNonTrackedQuery(string UserId) : IRequest<IEnumerable<TaskHierarchyLifeAreaDTO>>;
    public record AddTrackedQuery(string UserId, int taskId, DateTime StartTime, DateTime EndTime) : IRequest<TaskHierarchyLifeAreaDTO>;
    public record DeleteTaskItemQuery(string UserId, int taskID) : IRequest<TaskItemDTO>;
    public record RemoveFromTrackerQuery(string UserId, int taskID) : IRequest;
    public record AddToTrackerRequest(DateTime StartTime, DateTime EndTime);
}
