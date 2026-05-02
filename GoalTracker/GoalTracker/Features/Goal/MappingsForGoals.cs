using GoalTracker.Features.LifeArea;
using MediatR;
using System.Security.Claims;

namespace GoalTracker.Features.Goal
{
    public static class MappingsForGoals
    {
        public static void Map(WebApplication app)
        {
           AddGoalsEndpoint.Map(app);
            GetGoalsEndpoint.Map(app);
        }
    }
}
