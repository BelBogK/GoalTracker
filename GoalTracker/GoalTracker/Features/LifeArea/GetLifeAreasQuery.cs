using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.Extensions;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;
using static MudBlazor.CategoryTypes;

namespace GoalTracker.Features.LifeArea
{
    public record GetLifeAreasQuery(string UserId) : IRequest<IEnumerable<LifeAreaDTO>>;
    public record GetLifeAreaByIdQuery(string UserId, int lifeAreaId) : IRequest<LifeAreaDTO>;
    public class GetLifeAreaByIdHandler(ILifeAreaRepository repository, AppMapper mapper)
    : IRequestHandler<GetLifeAreaByIdQuery, LifeAreaDTO?>
    {
        public async Task<LifeAreaDTO?> Handle(
            GetLifeAreaByIdQuery request, CancellationToken ct)
        {
            var entity = await repository.GetByIdAsync(request.lifeAreaId);
            return entity is null ? null : mapper.ToDto(entity);
        }
    }
    public record GetGoalsByLifeAreaQuery(string UserId, int lifeAreaId) : IRequest<IEnumerable<GoalDTO>>;
    public class GetGoalsByLifeAreaHandler(IGoalRepository repository, AppMapper mapper)
     : IRequestHandler<GetGoalsByLifeAreaQuery, IEnumerable<GoalDTO>>
    {
        public async Task<IEnumerable<GoalDTO>> Handle(
            GetGoalsByLifeAreaQuery request, CancellationToken ct)
        {
            var goals = await repository.GetByLifeAreaAsync(request.UserId, request.lifeAreaId);
            return goals.Select(g => g.ToDto(mapper));
        }
    }
}
