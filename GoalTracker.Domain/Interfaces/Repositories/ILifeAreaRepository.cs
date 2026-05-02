using GoalTracker.Domain.Entities;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface ILifeAreaRepository:IBaseRepo<LifeArea>
    {
        Task<IEnumerable<LifeArea>> GetDefaultLifeAreas();
        Task<IEnumerable<LifeArea>> CreateRangeAsync(IEnumerable<LifeArea> entities);
    }
}
