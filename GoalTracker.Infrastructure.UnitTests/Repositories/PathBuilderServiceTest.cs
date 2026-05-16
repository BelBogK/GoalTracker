using GoalTracker.Data;
using GoalTracker.Infrastructure.Services;
using GoalTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.UnitTests.Repositories
{
    [TestClass]
    public class PathBuilderServiceTest
    {
        private static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseSqlServer( 
            )
       .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        [TestMethod]
        public async Task PathBuilder_GetPathToProjectConnectedToScen_Path()
        {
            var service = new PathBuilderService(CreateContext());

            var pathToProject=await service.BuildPathsAsync(1, PathEntityType.Project);
            Assert.IsNotNull(pathToProject);
        }
    }
}
