using GoalTracker.Data;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Features.Project
{
    public class GetProjectHandler(IProjectRepository repository, AppMapper mapper)
       : IRequestHandler<GetProjectQuery, IEnumerable<ProjectDTO>>
    {
        public async Task<IEnumerable<ProjectDTO>> Handle(
            GetProjectQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<GoalTracker.Domain.Entities.Project> projects;
            if(request.GoalId.HasValue)
            {
                projects = await repository.GetByGoalAsync(request.UserId, request.GoalId.Value);
            }
            else
            {
                projects = await repository.GetAllAsync(request.UserId);
            }
            return projects.Select(mapper.ToDto);
        }
        public class UpdateProjectHandler(IProjectRepository repository, AppMapper mapper)
    : IRequestHandler<UpdateProjectCommand, ProjectDTO>
        {
            public async Task<ProjectDTO> Handle(UpdateProjectCommand request, CancellationToken ct)
            {
                var entity = await repository.GetByIdAsync(request.Project.Id);
                if (entity is null) throw new Exception($"Project {request.Project.Id} not found");

                entity.Name = request.Project.Name;
                entity.Description = request.Project.Description;
                entity.Result = request.Project.Result;
                entity.CurrentStatus = request.Project.CurrentStatus;
                entity.GoalType = request.Project.GoalType;
                entity.Priority = request.Project.Priority;
                entity.StartTime = request.Project.StartTime;
                entity.EndTime = request.Project.EndTime;

                var updated = await repository.UpdateAsync(entity);
                return mapper.ToDto(updated);
            }
        }
        public class AddGoalToProjectHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
    : IRequestHandler<AddGoalToProjectCommand>
        {
            public async Task Handle(AddGoalToProjectCommand request, CancellationToken ct)
            {
                await using var context = await contextFactory.CreateDbContextAsync();

                var project = await context.Projects
                    .Include(p => p.Goals)
                    .FirstOrDefaultAsync(p => p.Id == request.ProjectId, ct);

                if (project is null) throw new Exception($"Project {request.ProjectId} not found");

                var goal = await context.Goals.FindAsync(request.GoalId);
                if (goal is null) throw new Exception($"Goal {request.GoalId} not found");

                project.Goals.Add(goal);
                await context.SaveChangesAsync(ct);
            }
        }

        public class RemoveGoalFromProjectHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
    : IRequestHandler<RemoveGoalFromProjectCommand>
        {
            public async Task Handle(RemoveGoalFromProjectCommand request, CancellationToken ct)
            {
                await using var context = await contextFactory.CreateDbContextAsync();

                var project = await context.Projects
                    .Include(p => p.Goals)
                    .FirstOrDefaultAsync(p => p.Id == request.ProjectId, ct);

                if (project is null) throw new Exception($"Project {request.ProjectId} not found");

                var goal = project.Goals.FirstOrDefault(g => g.Id == request.GoalId);
                if (goal is not null)
                {
                    project.Goals.Remove(goal);
                    await context.SaveChangesAsync(ct);
                }
            }
        }
        public class GetProjectByIdHandler(IProjectRepository repository, AppMapper mapper)
    : IRequestHandler<GetProjectByIdQuery, ProjectDTO?>
        {
            public async Task<ProjectDTO?> Handle(GetProjectByIdQuery request, CancellationToken ct)
            {
                var project = await repository.GetByIdAsync(request.ProjectId);
                return project is null ? null : mapper.ToDto(project);
            }
        }
    }
}
