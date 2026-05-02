using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.Goal
{
    public class AddGoalsEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/api/goals", async (
     HttpContext httpContext,
     IMediator mediator,
     ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<GoalDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddGoalCommand(userId, items!));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
