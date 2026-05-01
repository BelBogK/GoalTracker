using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Persistence.Configurations
{
    public class LifeAreaConfiguration : IEntityTypeConfiguration<LifeArea>
    {
        public void Configure(EntityTypeBuilder<LifeArea> builder)
        {
            // Оставляем CASCADE здесь — один путь
            builder
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            var userId = Guid.Empty;
            builder.HasData(new LifeArea
            {
                Name = "Body", 
                Description = "Физическое здоровье", 
                UserId=userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "Brain",
                Description = "То как хорошо работает мозг", 
                UserId = userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "MentalHealth",
                Description = "Душевное здоровье",
                UserId = userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "Money",
                Description = "Финансовое состояние",
                UserId = userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "Business",
                Description = "Бизнес и предпринимательство, как деньги приносит и развитие",
                UserId = userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "Family",
                Description = "Семейные отношения",
                UserId = userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "Romantic",
                Description = "Любовные отношения",
                UserId = userId.ToString()
            });

            builder.HasData(new LifeArea
            {
                Name = "Friendship",
                Description = "Дружеские отношения",
                UserId = userId.ToString()
            });
        }
    }
}
