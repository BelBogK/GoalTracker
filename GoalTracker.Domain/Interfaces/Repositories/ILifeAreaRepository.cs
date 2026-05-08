using GoalTracker.Domain.Entities;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface ILifeAreaRepository:IBaseRepo<LifeArea>
    {
        Task<IEnumerable<LifeArea>> GetDefaultLifeAreas();
        Task<IEnumerable<LifeArea>> CreateRangeAsync(IEnumerable<LifeArea> entities);
        Task<IEnumerable<LifeArea>> GetWithAllPathToTask(int taskId, string userId);
        Task<IEnumerable<LifeArea>> GetLifeAreasByTaskIdsAsync(IEnumerable<int> taskItemIds);

    }
}
