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
            //// Оставляем CASCADE здесь — один путь
            //builder
            //    .HasOne(l => l.User)
            //    .WithMany()
            //    .HasForeignKey(l => l.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var userId = Guid.Empty;
            //builder.HasData(new LifeArea
            //{
            //    Id = 1,
            //    Name = "Body",
            //    Description = "Физическое здоровье",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 2,
            //    Name = "Brain",
            //    Description = "То как хорошо работает мозг",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 3,
            //    Name = "MentalHealth",
            //    Description = "Душевное здоровье",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 4,
            //    Name = "Money",
            //    Description = "Финансовое состояние",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 5,
            //    Name = "Business",
            //    Description = "Бизнес и предпринимательство, как деньги приносит и развитие",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 6,
            //    Name = "Family",
            //    Description = "Семейные отношения",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 7,
            //    Name = "Romantic",
            //    Description = "Любовные отношения",
            //    UserId = userId.ToString()
            //});

            //builder.HasData(new LifeArea
            //{
            //    Id = 8,
            //    Name = "Friendship",
            //    Description = "Дружеские отношения",
            //    UserId = userId.ToString()
            //});
        }
    }
}
