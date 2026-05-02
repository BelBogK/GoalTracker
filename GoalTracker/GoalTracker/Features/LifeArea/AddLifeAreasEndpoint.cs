using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;
using System.Text.Json;

namespace GoalTracker.Features.LifeArea
{
    public class AddLifeAreasEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/api/lifeareas", async (
     HttpContext httpContext,
     IMediator mediator,
     ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<IEnumerable<LifeAreaDTO>>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddLifeAreasCommand(userId, items!));
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}
