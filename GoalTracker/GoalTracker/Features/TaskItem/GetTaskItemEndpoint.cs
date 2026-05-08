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


            app.MapGet("/api/tasks/daily-tracker", async (
DateTime startTime,
    DateTime endTime,
     IMediator mediator,
     ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;

                var result = await mediator.Send(
                    new GetDailyTrackerQuery(userId, startTime, endTime));

                return Results.Ok(result);
            })
 .RequireAuthorization();

            app.MapGet("/api/tasks/non-tracked", async (
               IMediator mediator,
               ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetNonTrackedQuery(userId));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/tasks/{taskId}/tracker/{start}", async (
                int taskId,
                DateTime start,
               IMediator mediator,
               ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddTrackedQuery(userId, taskId, start));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapDelete("/api/tasks/{taskId}/tracker", async (
                int taskId,
               IMediator mediator,
               ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new DeleteTaskItemQuery(userId,taskId));
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

            app.MapDelete("/api/tasks/{taskId}", async (
                int taskId,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new DeleteTaskItemQuery(userId,taskId));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
