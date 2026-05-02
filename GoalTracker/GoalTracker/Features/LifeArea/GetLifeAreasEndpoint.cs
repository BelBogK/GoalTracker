using MediatR;
using System.Security.Claims;

namespace GoalTracker.Features.LifeArea
{
    public static class GetLifeAreasEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/lifeareas", async (
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetLifeAreasQuery(userId));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
