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
            app.MapGet("/api/lifeareas/{id}", async (
    int id,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetLifeAreaByIdQuery(userId, id));
                return result is null ? Results.NotFound() : Results.Ok(result);
            }).RequireAuthorization();

            app.MapGet("/api/lifeareas/{id}/goals", async (
                int id,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetGoalsByLifeAreaQuery(userId, id));
                return Results.Ok(result);
            }).RequireAuthorization();
        }


    }
}
