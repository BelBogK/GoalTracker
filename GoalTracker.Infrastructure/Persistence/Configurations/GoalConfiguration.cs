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
            builder
                .HasMany(g => g.LifeAreas)
                .WithMany(l => l.Goals)
                .UsingEntity(j => j.ToTable("GoalLifeArea"));

            // Отключаем CASCADE для связи Goal -> LifeArea
            builder
                .HasMany(g => g.LifeAreas)
                .WithMany(l => l.Goals)
                .UsingEntity(j =>
                {
                    j.ToTable("GoalLifeArea");
                    j.HasOne(typeof(LifeArea))
                        .WithMany()
                        .OnDelete(DeleteBehavior.NoAction);
                });
        }
    }
}
