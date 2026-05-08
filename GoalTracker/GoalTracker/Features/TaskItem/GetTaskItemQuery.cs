using GoalTracker.Shared;
using GoalTracker.Shared.SuperClass;
using MediatR;

namespace GoalTracker.Features.TaskItem
{
    public record GetTaskItemQuery(string UserId, int? ProjectId = null) : IRequest<IEnumerable<TaskItemDTO>>;
    public record GetDailyTrackerQuery(string UserId, DateTime start, DateTime end) : IRequest<IEnumerable<TaskHierarchyLifeAreaDTO>>;
    public record GetNonTrackedQuery(string UserId) : IRequest<IEnumerable<TaskHierarchyLifeAreaDTO>>;
    public record AddTrackedQuery(string UserId, int taskId, DateTime when) : IRequest<TaskHierarchyLifeAreaDTO>;
    public record DeleteTaskItemQuery(string UserId, int taskID) : IRequest<TaskItemDTO>;
    public record RemoveFromTrackerAsync(string UserId, int taskID) : IRequest;
}
