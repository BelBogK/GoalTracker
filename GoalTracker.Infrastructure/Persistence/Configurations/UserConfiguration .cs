using GoalTracker.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalTracker.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<GoalTrackerUser>
    {
        public void Configure(EntityTypeBuilder<GoalTrackerUser> builder)
        {
            var defaultUserId = "00000000-0000-0000-0000-000000000000";

            builder.HasData(new GoalTrackerUser
            {
                Id = defaultUserId,
                UserName = "admin@goaltracker.com",
                NormalizedUserName = "ADMIN@GOALTRACKER.COM",
                Email = "admin@goaltracker.com",
                NormalizedEmail = "ADMIN@GOALTRACKER.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "AQAAAAIAAYagAAAAE..." // хэш пароля
            });
        }
    }
}
