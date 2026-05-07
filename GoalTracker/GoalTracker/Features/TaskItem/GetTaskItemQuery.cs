using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.TaskItem
{
    public record GetTaskItemQuery(string UserId, int? ProjectId = null) : IRequest<IEnumerable<TaskItemDTO>>;
    public record DeleteTaskItemQuery(string UserId, int taskID) : IRequest<TaskItemDTO>;
}
