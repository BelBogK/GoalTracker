using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.LifeArea
{
    public class AddLifeAreasHandler(ILifeAreaRepository repository)
     : IRequestHandler<AddLifeAreasCommand, IEnumerable<LifeAreaDTO>>
    {
        public async Task<IEnumerable<LifeAreaDTO>> Handle(
            AddLifeAreasCommand request,
            CancellationToken cancellationToken)
        {
            var entities = request.Items.Select(dto => new GoalTracker.Domain.Entities.LifeArea
            {
                Name = dto.Name,
                Description = dto.Description,
                IdealVision = dto.IdealVision,
                UserId = request.UserId
            }).ToList();


            var personalLifeArea = await repository.CreateRangeAsync(entities);


            return personalLifeArea.Select(a => new LifeAreaDTO
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                IdealVision = a.IdealVision,
                IsDefault = false
            });
        }
    }
}
