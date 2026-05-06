using GoalTracker.Client.Pages;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Mapper;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.Goal
{
    public class GetGoalHandler(IGoalRepository repository, AppMapper mapper)
       : IRequestHandler<GetGoalsQuery, IEnumerable<GoalDTO>>
    {
        public async Task<IEnumerable<GoalDTO>> Handle(
            GetGoalsQuery request,
            CancellationToken cancellationToken)
        {
            var goals = await repository.GetAllAsync(request.UserId); 
         
            var result=new List<GoalDTO>();
            foreach (var goal in goals)
            {
                var item = new GoalDTO()
                {
                    Description = goal.Description,
                    StartDate = goal.StartDate,
                    TargetDate = goal.TargetDate,
                    CurrentStatus = goal.CurrentStatus,
                    GoalType = goal.GoalType,
                    Id = goal.Id,
                    IdealVision = goal.IdealVision,
                    Name = goal.Name,
                    Priority = goal.Priority,
                    Reward = goal.Reward
                };
                foreach (var sc in goal.Scenarios)
                {
                    var goalSc = new GoalScenarioDTO()
                    {
                        Description = sc.Description,
                        Id = sc.Id,
                        IsActive = sc.IsActive,
                        Name = sc.Name,
                        Projects = sc.Projects.Select(mapper.ToDto).ToList()
                    };
                    foreach (var cr in sc.ChildRelations)
                    {
                        var cr2 = new GoalScenarioDTO()
                        {
                            Description = cr.Child.Description,
                            Id = cr.Child.Id,
                            IsActive = cr.Child.IsActive,
                            Name = cr.Child.Name,
                            Projects = cr.Child.Projects.Select(mapper.ToDto).ToList()
                        };
                        goalSc.ChildRelations = [cr2];

                    }
                    item.Scenarios.Add(goalSc);
                }
                result.Add(item);
            }

            return result;
        }
    }
}
