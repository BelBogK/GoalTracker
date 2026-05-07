using GoalTracker.Features.Goal;
using GoalTracker.Features.LifeArea;
using GoalTracker.Shared;
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

            app.MapGet("/api/scens/{scenId}/projects", async (
              int scenId,
              IMediator mediator,
              ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetProjectsByScenQuery(userId, scenId));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapGet("/api/projects/{id}", async (
    int id,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new GetProjectByIdQuery(userId, id));
                return result is null ? Results.NotFound() : Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/projects", async (
HttpContext httpContext,
IMediator mediator,
ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<ProjectDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddProjectCommand(userId, items!, null));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/goals/{goalId}/projects", async (
                int goalId,
    HttpContext httpContext,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<ProjectDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddProjectCommand(userId, items!, goalId));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/scens/{scenId}/projects", async (
                int scenId,
    HttpContext httpContext,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var items = await httpContext.Request.ReadFromJsonAsync<ProjectDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new AddProjectToScenCommand(userId, items!, scenId));
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPut("/api/projects/{id}", async (
    int id,
    HttpContext httpContext,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var item = await httpContext.Request.ReadFromJsonAsync<ProjectDTO>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var result = await mediator.Send(new UpdateProjectCommand(userId, item!));
                return Results.Ok(result);
            }).RequireAuthorization();
            app.MapPost("/api/projects/{projectId}/goals/{goalId}", async (
    int projectId,
    int goalId,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await mediator.Send(new AddGoalToProjectCommand(userId, projectId, goalId));
                return Results.Ok();
            }).RequireAuthorization();
            app.MapDelete("/api/projects/{projectId}/goals/{goalId}", async (
    int projectId,
    int goalId,
    IMediator mediator,
    ClaimsPrincipal user) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await mediator.Send(new RemoveGoalFromProjectCommand(userId, projectId, goalId));
                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}
