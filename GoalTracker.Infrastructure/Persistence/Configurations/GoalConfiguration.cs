using GoalTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Persistence.Configurations
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            // Отключаем cascade для связи Goal -> User
            builder
                .HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Many-to-Many с LifeArea
            builder
                .HasMany(g => g.LifeAreas)
                .WithMany(l => l.Goals)
                .UsingEntity(j => j.ToTable("GoalLifeArea"));
        }
    }
}
