using GoalTracker.Features.Goal;
using GoalTracker.Features.TaskItem;

namespace GoalTracker.Features
{
    public static class MappingForItems
    {
        public static void Map(WebApplication app)
        {
            MappingsForGoals.Map(app);
            LifeArea.MappingsForLifeArea.Map(app);
            MappingsForProject.Map(app);
            MappingsForTaskItem.Map(app);
        }
    }
}
