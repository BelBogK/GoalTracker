using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.GoalScenario
{
    public class GoalScenarioEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Get all scenarios for a goal
            app.MapGet("/api/goals/{goalId}/scenarios", async (
                int goalId,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetScenariosByGoalQuery(userId, goalId));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapGet("/api/scenarios/{scenId}", async (
               int scenId,
               IMediator mediator,
               ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetScenariosByIdQuery(userId, scenId));
                return Results.Ok(result);
            }).RequireAuthorization();

            // Add scenario to goal
            app.MapPost("/api/goals/{goalId}/scenarios", async (
                int goalId,
                HttpContext httpContext,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var item = await httpContext.Request.ReadFromJsonAsync<GoalScenarioDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddGoalScenarioCommand(userId, goalId, item!));
                return Results.Ok(result);
            }).RequireAuthorization();

            // Add child scenario to parent
            app.MapPost("/api/scenarios/{parentId}/children", async (
                int parentId,
                HttpContext httpContext,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var item = await httpContext.Request.ReadFromJsonAsync<GoalScenarioDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddChildScenarioCommand(userId, parentId, item!));
                return Results.Ok(result);
            }).RequireAuthorization();

            // Update scenario
            app.MapPut("/api/scenarios/{id}", async (
                int id,
                HttpContext httpContext,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var item = await httpContext.Request.ReadFromJsonAsync<GoalScenarioDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new UpdateGoalScenarioCommand(userId, item!));
                return Results.Ok(result);
            }).RequireAuthorization();

            // Delete scenario
            app.MapDelete("/api/scenarios/{id}", async (
                int id,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await mediator.Send(new DeleteGoalScenarioCommand(userId, id));
                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}
