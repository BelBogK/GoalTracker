namespace GoalTracker.Features.LifeArea
{
    public static class MappingsForLifeArea
    {
        public static void Map(WebApplication app)
        {
            GetLifeAreasEndpoint.Map(app);
            AddLifeAreasEndpoint.Map(app);
        }
    }
}
