using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;
using static GoalTracker.Features.DayComment.GetDayCommentsHandler;

namespace GoalTracker.Features.DayComment
{
    public class MapDayComment
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/daycomment/period", async (
    DateOnly startDate,
    DateOnly endDate,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetDayCommentsQuery(userId, startDate, endDate));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/daycomment", async (
                DayCommentDTO dto,
                IMediator mediator,
                ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await mediator.Send(new SaveDayCommentCommand(userId, dto));
                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}
