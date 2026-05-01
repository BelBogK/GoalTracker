using GoalTracker.Domain.Entities;
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
        }
    }
}
