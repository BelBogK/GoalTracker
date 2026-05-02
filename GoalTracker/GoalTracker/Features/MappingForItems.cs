using GoalTracker.Features.Goal;

namespace GoalTracker.Features
{
    public static class MappingForItems
    {
        public static void Map(WebApplication app)
        {
            LifeArea.MappingsForLifeArea.Map(app);
            MappingsForGoals.Map(app);
        }
    }
}
