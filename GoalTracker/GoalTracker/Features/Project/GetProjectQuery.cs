using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Project
{
    public record GetProjectQuery(string UserId, int? GoalId = null) : IRequest<IEnumerable<ProjectDTO>>;
    public record GetProjectsByScenQuery(string UserId, int? scenId = null) : IRequest<IEnumerable<ProjectDTO>>;
    public record GetProjectByIdQuery(string UserId, int ProjectId) : IRequest<ProjectDTO?>;
    public record UpdateProjectCommand(string UserId, ProjectDTO Project) : IRequest<ProjectDTO>;
    public record AddGoalToProjectCommand(string UserId, int ProjectId, int GoalId) : IRequest;
    public record RemoveGoalFromProjectCommand(string UserId, int ProjectId, int GoalId) : IRequest;
}
