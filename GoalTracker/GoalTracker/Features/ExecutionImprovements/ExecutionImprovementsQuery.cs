using GoalTracker.Data;
using GoalTracker.Shared.Enums;
using GoalTracker.Shared.ImproveWorking;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Features.ExecutionImprovements
{
    // GetAllExecutionImprovementsQuery
    public record GetAllExecutionImprovementsQuery(string UserId) : IRequest<List<ExecutionImprovementDTO>>;

    public class GetAllExecutionImprovementsHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetAllExecutionImprovementsQuery, List<ExecutionImprovementDTO>>
    {
        public async Task<List<ExecutionImprovementDTO>> Handle(GetAllExecutionImprovementsQuery request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);
            return await context.ExecutionImprovements
                .Where(e => e.UserId == request.UserId)
                .Include(e => e.HistoreImproveds)
                .Select(e => new ExecutionImprovementDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    HistoreImproveds = e.HistoreImproveds.Select(h => new HistoreImprovedDTO
                    {
                        Id = h.Id,
                        StartImprove = h.StartImprove,
                        Comments = h.Comments,
                        Result = h.Result,
                        IsImproved = h.IsImproved,
                        TrackedEntitiesType = h.TrackedEntitiesType,
                        EntityeId = h.EntityeId
                    }).ToList()
                })
                .ToListAsync(ct);
        }
    }

    // GetExecutionImprovementByIdQuery
    public record GetExecutionImprovementByIdQuery(int Id, string UserId) : IRequest<ExecutionImprovementDTO?>;

    public class GetExecutionImprovementByIdHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetExecutionImprovementByIdQuery, ExecutionImprovementDTO?>
    {
        public async Task<ExecutionImprovementDTO?> Handle(GetExecutionImprovementByIdQuery request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);
            return await context.ExecutionImprovements
                .Where(e => e.Id == request.Id && e.UserId == request.UserId)
                .Include(e => e.HistoreImproveds)
                .Select(e => new ExecutionImprovementDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    HistoreImproveds = e.HistoreImproveds.Select(h => new HistoreImprovedDTO
                    {
                        Id = h.Id,
                        StartImprove = h.StartImprove,
                        Comments = h.Comments,
                        Result = h.Result,
                        IsImproved = h.IsImproved,
                        TrackedEntitiesType = h.TrackedEntitiesType,
                        EntityeId = h.EntityeId
                    }).ToList()
                })
                .FirstOrDefaultAsync(ct);
        }
    }

    // GetExecutionImprovementByEntityQuery
    public record GetExecutionImprovementByEntityQuery(int EntityId, TrackedEntitiesType Type, string UserId)
        : IRequest<ExecutionImprovementDTO?>;

    public class GetExecutionImprovementByEntityHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetExecutionImprovementByEntityQuery, ExecutionImprovementDTO?>
    {
        public async Task<ExecutionImprovementDTO?> Handle(GetExecutionImprovementByEntityQuery request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);
            return await context.ExecutionImprovements
                .Where(e => e.UserId == request.UserId)
                .Include(e => e.HistoreImproveds.Where(h =>
                    h.EntityeId == request.EntityId && h.TrackedEntitiesType == request.Type))
                .Select(e => new ExecutionImprovementDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    HistoreImproveds = e.HistoreImproveds
                        .Where(h => h.EntityeId == request.EntityId && h.TrackedEntitiesType == request.Type)
                        .Select(h => new HistoreImprovedDTO
                        {
                            Id = h.Id,
                            StartImprove = h.StartImprove,
                            Comments = h.Comments,
                            Result = h.Result,
                            IsImproved = h.IsImproved,
                            TrackedEntitiesType = h.TrackedEntitiesType,
                            EntityeId = h.EntityeId
                        }).ToList()
                })
                .FirstOrDefaultAsync(ct);
        }
    }
    public record GetImprovementsByEntityQuery(int EntityId, TrackedEntitiesType Type, string UserId)
    : IRequest<List<ExecutionImprovementDTO>>;

    public class GetImprovementsByEntityHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetImprovementsByEntityQuery, List<ExecutionImprovementDTO>>
    {
        public async Task<List<ExecutionImprovementDTO>> Handle(GetImprovementsByEntityQuery request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);

            return await context.ExecutionImprovements
                .Where(e => e.UserId == request.UserId &&
                            e.HistoreImproveds.Any(h =>
                                h.EntityeId == request.EntityId &&
                                h.TrackedEntitiesType == request.Type))
                .Select(e => new ExecutionImprovementDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    HistoreImproveds = e.HistoreImproveds
                        .Where(h => h.EntityeId == request.EntityId && h.TrackedEntitiesType == request.Type)
                        .Select(h => new HistoreImprovedDTO
                        {
                            Id = h.Id,
                            StartImprove = h.StartImprove,
                            Comments = h.Comments,
                            Result = h.Result,
                            IsImproved = h.IsImproved,
                            TrackedEntitiesType = h.TrackedEntitiesType,
                            EntityeId = h.EntityeId
                        }).ToList()
                })
                .ToListAsync(ct);
        }
    }
}
