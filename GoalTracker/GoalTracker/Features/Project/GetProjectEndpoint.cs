using GoalTracker.Features.LifeArea;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.Project
{
    public static class GetProjectEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/projects", async (
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetProjectQuery(userId));
                return Results.Ok(result);
            }).RequireAuthorization();

            // Проекты по GoalId
            app.MapGet("/api/goals/{goalId}/projects", async (
                int goalId,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetProjectQuery(userId, goalId));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
