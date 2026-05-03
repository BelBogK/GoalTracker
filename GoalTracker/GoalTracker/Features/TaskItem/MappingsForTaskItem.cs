namespace GoalTracker.Features.TaskItem
{
    public static class MappingsForTaskItem
    {
        public static void Map(WebApplication app)
        {
            AddTaskItemEndpoint.Map(app);
            GetTaskItemEndpoint.Map(app);
        }
    }
}
