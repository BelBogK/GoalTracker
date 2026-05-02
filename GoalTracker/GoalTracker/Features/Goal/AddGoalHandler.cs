using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public class AddGoalHandler(IGoalRepository repository, AppMapper mapper)
     : IRequestHandler<AddGoalCommand, GoalDTO>
    {
        public async Task<GoalDTO> Handle(AddGoalCommand request, CancellationToken cancellationToken)
        {
            var item = mapper.ToEntity(request.Goal);
            if (item == null)
            {
                return null;
            }
            item.UserId = request.UserId;
            var result = await repository.CreateAsync(item);
            return mapper.ToDto(result);
        }
    }
}
