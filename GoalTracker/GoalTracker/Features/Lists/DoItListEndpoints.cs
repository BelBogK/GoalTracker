using GoalTracker.Shared;
using MediatR;
using System.Security.Claims;

namespace GoalTracker.Features.Lists
{
    public static class DoItListEndpoints
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/do-it-lists").RequireAuthorization();

            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id:int}", Update);
            group.MapDelete("/{id:int}", Delete);

            // Items
            group.MapPost("/{listId:int}/items", AddItem);
            group.MapPut("/{listId:int}/items/{itemId:int}", UpdateItem);
            group.MapDelete("/{listId:int}/items/{itemId:int}", DeleteItem);
        }

        private static async Task<IResult> GetAll(IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Results.Ok(await mediator.Send(new GetDoItListsQuery(userId)));
        }
        private static async Task<IResult> GetById(int id, IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Results.Ok(await mediator.Send(new GetDoItListByIdQuery(id, userId)));
        }

        private static async Task<IResult> Create(DoItListDTO dto, IMediator mediator,ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Results.Ok(await mediator.Send(new CreateDoItListCommand(dto, userId)));
        } 

        private static async Task<IResult> Update(
            int id,
            DoItListDTO dto,
            IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await mediator.Send(new UpdateDoItListCommand(id, dto, userId));

            return Results.NoContent();
        }

        private static async Task<IResult> Delete(int id, IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await mediator.Send(new DeleteDoItListCommand(id, userId));
            return Results.NoContent();
        }

        private static async Task<IResult> AddItem(int listId, DoItListItemDTO dto, IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Results.Ok(await mediator.Send(new AddDoItListItemCommand(userId,listId, dto)));
        } 

        private static async Task<IResult> UpdateItem(int listId, int itemId, DoItListItemDTO dto, IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await mediator.Send(new UpdateDoItListItemCommand(userId,listId, itemId, dto));

            return Results.NoContent();
        } 

        private static async Task<IResult> DeleteItem(int listId, int itemId, IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await mediator.Send(new DeleteDoItListItemCommand( userId,listId, itemId));
            return Results.NoContent();
        }


    }
}
