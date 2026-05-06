using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.Goal
{
    public class AddProjectEndpoint
    {
        public static void Map(WebApplication app)
        {
    //        app.MapPost("/api/projects", async (
    // HttpContext httpContext,
    // IMediator mediator,
    // ClaimsPrincipal user) =>
    //        {
    //            var items = await httpContext.Request.ReadFromJsonAsync<ProjectDTO>(
    //                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    //            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
    //            var result = await mediator.Send(new AddProjectCommand(userId, items!,null));
    //            return Results.Ok(result);
    //        }).RequireAuthorization();

    //        app.MapPost("/api/goals/{goalId}/projects", async (
    //            int goalId,
    //HttpContext httpContext,
    //IMediator mediator,
    //ClaimsPrincipal user) =>
    //        {
    //            var items = await httpContext.Request.ReadFromJsonAsync<ProjectDTO>(
    //                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    //            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
    //            var result = await mediator.Send(new AddProjectCommand(userId, items!, goalId));
    //            return Results.Ok(result);
    //        }).RequireAuthorization();
        }
    }
}
