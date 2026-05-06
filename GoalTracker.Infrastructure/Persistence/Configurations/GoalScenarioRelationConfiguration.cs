using GoalTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure.Persistence.Configurations
{
    public class GoalScenarioRelationConfiguration : IEntityTypeConfiguration<GoalScenarioRelation>
    {
        public void Configure(EntityTypeBuilder<GoalScenarioRelation> builder)
        {
            builder.HasOne(r => r.Parent)
                .WithMany(s => s.ChildRelations)
                .HasForeignKey(r => r.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Child)
                .WithMany(s => s.ParentRelations)
                .HasForeignKey(r => r.ChildId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
