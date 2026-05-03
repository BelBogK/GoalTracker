using GoalTracker.Features.LifeArea;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.TaskItem
{
    public static class GetTaskItemEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/tasks", async (
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetTaskItemQuery(userId));
                return Results.Ok(result);
            }).RequireAuthorization();

            // Проекты по GoalId
            app.MapGet("/api/project/{projectId}/tasks", async (
                int projectId,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetTaskItemQuery(userId, projectId));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
