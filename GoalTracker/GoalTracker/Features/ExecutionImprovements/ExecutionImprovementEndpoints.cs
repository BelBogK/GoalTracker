using GoalTracker.Shared.Enums;
using GoalTracker.Shared.ImproveWorking;
using MediatR;
using System.Security.Claims;

namespace GoalTracker.Features.ExecutionImprovements
{
    public class ExecutionImprovementEndpoints
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/execution-improvements").RequireAuthorization();
            group.MapGet("/by-entity/{entityId:int}", async (int entityId, TrackedEntitiesType type, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new GetImprovementsByEntityQuery(entityId, type, userId)));
            });
            group.MapPost("/", async (ExecutionImprovementDTO dto, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new CreateExecutionImprovementCommand(dto, userId)));
            });
            group.MapGet("/", async (ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new GetAllExecutionImprovementsQuery(userId)));
            });

            group.MapGet("/{id:int}", async (int id, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new GetExecutionImprovementByIdQuery(id, userId)));
            });

            group.MapGet("/entity/{entityId:int}", async (int entityId, TrackedEntitiesType type, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new GetExecutionImprovementByEntityQuery(entityId, type, userId)));
            });

            group.MapPost("/{improvementId:int}/history", async (int improvementId, HistoreImprovedDTO dto, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new AddHistoreImprovedCommand(improvementId, dto, userId)));
            });

            group.MapPut("/{improvementId:int}/history/{historyId:int}", async (int improvementId, int historyId, HistoreImprovedDTO dto, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return Results.Ok(await m.Send(new UpdateHistoreImprovedCommand(improvementId, historyId, dto, userId)));
            });

            group.MapDelete("/{improvementId:int}/history/{historyId:int}", async (int improvementId, int historyId, ClaimsPrincipal user, IMediator m) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await m.Send(new DeleteHistoreImprovedCommand(improvementId, historyId, userId));
                return Results.NoContent();
            });
        }
    }
}
