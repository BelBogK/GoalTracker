using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.TaskItem
{
    public record AddTaskItemCommand(
      string UserId,
      TaskItemDTO task,
      int? projectId) : IRequest<TaskItemDTO>;
     
}
