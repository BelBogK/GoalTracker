using GoalTracker.Data;
using GoalTracker.Domain.Entities.ImproveWorking;
using GoalTracker.Shared.ImproveWorking;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Features.ExecutionImprovements
{
    // AddHistoreImprovedCommand
    public record AddHistoreImprovedCommand(int ImprovementId, HistoreImprovedDTO Dto, string UserId)
        : IRequest<HistoreImprovedDTO>;

    public class AddHistoreImprovedHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<AddHistoreImprovedCommand, HistoreImprovedDTO>
    {
        public async Task<HistoreImprovedDTO> Handle(AddHistoreImprovedCommand request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);

            var improvement = await context.ExecutionImprovements
                .FirstOrDefaultAsync(e => e.Id == request.ImprovementId && e.UserId == request.UserId, ct)
                ?? throw new KeyNotFoundException("Improvement not found.");

            var entity = new HistoreImproved
            {
                StartImprove = request.Dto.StartImprove,
                Comments = request.Dto.Comments,
                Result = request.Dto.Result,
                IsImproved = request.Dto.IsImproved,
                TrackedEntitiesType = request.Dto.TrackedEntitiesType,
                EntityeId = request.Dto.EntityeId,
                UserId = request.UserId,
                Created = DateTime.UtcNow
            };

            improvement.HistoreImproveds.Add(entity);
            await context.SaveChangesAsync(ct);

            request.Dto.Id = entity.Id;
            return request.Dto;
        }
    }
    public record CreateExecutionImprovementCommand(ExecutionImprovementDTO Dto, string UserId)
    : IRequest<ExecutionImprovementDTO>;

    public class CreateExecutionImprovementHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<CreateExecutionImprovementCommand, ExecutionImprovementDTO>
    {
        public async Task<ExecutionImprovementDTO> Handle(CreateExecutionImprovementCommand request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);

            var entity = new ExecutionImprovement
            {
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                UserId = request.UserId,
                Created = DateTime.UtcNow
            };

            context.ExecutionImprovements.Add(entity);
            await context.SaveChangesAsync(ct);

            request.Dto.Id = entity.Id;
            return request.Dto;
        }
    }
    // UpdateHistoreImprovedCommand
    public record UpdateHistoreImprovedCommand(int ImprovementId, int HistoryId, HistoreImprovedDTO Dto, string UserId)
        : IRequest<HistoreImprovedDTO>;

    public class UpdateHistoreImprovedHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<UpdateHistoreImprovedCommand, HistoreImprovedDTO>
    {
        public async Task<HistoreImprovedDTO> Handle(UpdateHistoreImprovedCommand request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);

            var entity = await context.HistoreImproveds
                .FirstOrDefaultAsync(h => h.Id == request.HistoryId && h.UserId == request.UserId, ct)
                ?? throw new KeyNotFoundException("History entry not found.");

            entity.StartImprove = request.Dto.StartImprove;
            entity.Comments = request.Dto.Comments;
            entity.Result = request.Dto.Result;
            entity.IsImproved = request.Dto.IsImproved;
            entity.TrackedEntitiesType = request.Dto.TrackedEntitiesType;
            entity.EntityeId = request.Dto.EntityeId;

            await context.SaveChangesAsync(ct);
            return request.Dto;
        }
    }

    // DeleteHistoreImprovedCommand
    public record DeleteHistoreImprovedCommand(int ImprovementId, int HistoryId, string UserId) : IRequest;

    public class DeleteHistoreImprovedHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<DeleteHistoreImprovedCommand>
    {
        public async Task Handle(DeleteHistoreImprovedCommand request, CancellationToken ct)
        {
            await using var context = await contextFactory.CreateDbContextAsync(ct);

            var entity = await context.HistoreImproveds
                .FirstOrDefaultAsync(h => h.Id == request.HistoryId && h.UserId == request.UserId, ct)
                ?? throw new KeyNotFoundException("History entry not found.");

            context.HistoreImproveds.Remove(entity);
            await context.SaveChangesAsync(ct);
        }
    }
}
