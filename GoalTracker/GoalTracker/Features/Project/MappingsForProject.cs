using GoalTracker.Features.LifeArea;
using GoalTracker.Features.Project;
using MediatR;
using System.Security.Claims;

namespace GoalTracker.Features.Goal
{
    public static class MappingsForProject
    {
        public static void Map(WebApplication app)
        {
            AddProjectEndpoint.Map(app);
            GetProjectEndpoint.Map(app);
        }
    }
}
