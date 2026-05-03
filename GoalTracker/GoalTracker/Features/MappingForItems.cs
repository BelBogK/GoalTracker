using GoalTracker.Features.Goal;

namespace GoalTracker.Features
{
    public static class MappingForItems
    {
        public static void Map(WebApplication app)
        {
            MappingsForGoals.Map(app);
            LifeArea.MappingsForLifeArea.Map(app);
            MappingsForProject.Map(app);
        }
    }
}
