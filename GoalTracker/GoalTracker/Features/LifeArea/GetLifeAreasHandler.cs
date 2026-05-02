using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.LifeArea
{
    public class GetLifeAreasHandler(ILifeAreaRepository repository)
       : IRequestHandler<GetLifeAreasQuery, IEnumerable<LifeAreaDTO>>
    {
        public async Task<IEnumerable<LifeAreaDTO>> Handle(
            GetLifeAreasQuery request,
            CancellationToken cancellationToken)
        {
            var areas = await repository.GetAllAsync(request.UserId);
            if (!areas.Any())
            {
                areas = await repository.GetDefaultLifeAreas();
            }

            return areas.Select(a => new LifeAreaDTO
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                IdealVision = a.IdealVision,
                IsDefault = a.UserId==Guid.Empty.ToString()
            });
        }
    }
}
