using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GoalTracker.Infrastructure.UnitTests
{
    [TestClass]
    public class LifeAreaRepositoryTests
    {
        private static ApplicationDbContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new ApplicationDbContext(options);
        }

        [TestMethod]
        public async Task GetWithAllPathToTask_TaskTrackerExists_TaskAndUserExist_ReturnsEmptyList()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateInMemoryContext(dbName);

            var taskId = 1;
            var userId = "user-1";

            var task = new TaskItem { Id = taskId, UserId = userId, Name = "T" };
            var project = new Project { Id = 10, Name = "P" };
            var goal = new Goal { Id = 20, Name = "G" };
            var lifeArea = new LifeArea { Id = 30, Name = "L", UserId = userId };

            // Build relationships
            goal.LifeAreas.Add(lifeArea);
            project.Goals.Add(goal);
            task.Projects.Add(project);

            context.TaskItems.Add(task);
            context.Projects.Add(project);
            context.Goals.Add(goal);
            context.LifeAreas.Add(lifeArea);

            // Add a daily tracker for the task
            var tracker = new TaskDailyTracker { Id = 100, TaskItemId = taskId, Created = DateTime.UtcNow, UserId = userId, TodayIs = DateTime.UtcNow };
            context.DailyTrackers.Add(tracker);

            await context.SaveChangesAsync();

            var mockFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(context);

            var repo = new LifeAreaRepository(mockFactory.Object);

            // Act
            var result = await repo.GetWithAllPathToTask(taskId, userId);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<LifeArea>(), result.ToList());
        }

    }
}
