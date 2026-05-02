using GoalTracker.Features.LifeArea;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.Goal
{
    public class GetGoalsEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/goals", async (
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetGoalsQuery(userId));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
