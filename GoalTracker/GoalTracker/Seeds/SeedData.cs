using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var defaultUserId = Guid.Empty.ToString();

        // USER
        var userExists = await context.Users
            .AnyAsync(x => x.Id == defaultUserId);

        if (!userExists)
        {
            context.Users.Add(new GoalTrackerUser
            {
                Id = defaultUserId,
                UserName = "UserForDefaultUsersData",
                NormalizedUserName = "USERFORDEFAULTUSERSDATA",
                Email = "default@local.local",
                NormalizedEmail = "DEFAULT@LOCAL.LOCAL",
                EmailConfirmed = true
            });

            await context.SaveChangesAsync();
        }

        var existDefaultDataForLifeArea=await context.LifeAreas.AnyAsync(x=>x.UserId== defaultUserId);

        if(existDefaultDataForLifeArea)
        {
            return;
        }

        // LIFE AREAS
        var lifeAreas = new List<LifeArea>
        {
            new()
            {
                Name = "Body",
                Description = "Физическое здоровье",
                UserId = defaultUserId
            },
            new()
            {
                Name = "Brain",
                Description = "То как хорошо работает мозг",
                UserId = defaultUserId
            },
            new()
            {
                Name = "MentalHealth",
                Description = "Душевное здоровье",
                UserId = defaultUserId
            },
            new()
            {
                Name = "Money",
                Description = "Финансовое состояние",
                UserId = defaultUserId
            },
            new()
            {
                Name = "Business",
                Description = "Бизнес и предпринимательство, как деньги приносит и развитие",
                UserId = defaultUserId
            },
            new()
            {
                Name = "Family",
                Description = "Семейные отношения",
                UserId = defaultUserId
            },
            new()
            {
                Name = "Romantic",
                Description = "Любовные отношения",
                UserId = defaultUserId
            },
            new()
            {
                Name = "Friendship",
                Description = "Дружеские отношения",
                UserId = defaultUserId
            }
        };

        foreach (var area in lifeAreas)
        {
            var exists = await context.LifeAreas
                .AnyAsync(x =>
                    x.Name == area.Name &&
                    x.UserId == area.UserId);

            if (!exists)
            {
                context.LifeAreas.Add(area);
            }
        }

        await context.SaveChangesAsync();
    }
}