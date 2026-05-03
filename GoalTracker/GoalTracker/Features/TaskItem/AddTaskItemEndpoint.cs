using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.TaskItem
{
    public class AddTaskItemEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/api/tasks", async (
     HttpContext httpContext,
     IMediator mediator,
     ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<TaskItemDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddTaskItemCommand(userId, items!,null));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/project/{projectId}/tasks", async (
                int projectId,
    HttpContext httpContext,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<TaskItemDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddTaskItemCommand(userId, items!, projectId));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
