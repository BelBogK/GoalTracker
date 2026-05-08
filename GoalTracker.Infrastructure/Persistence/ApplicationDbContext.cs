using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace GoalTracker.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<GoalTrackerUser>(options)
    {
        public DbSet<LifeArea> LifeAreas { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalScenario> GoalScenarios { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanItem> PlanItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AlternativeScenarioProject> AlternativeScenarioProjects { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<GoalScenario> Scenarios { get; set; }
        public DbSet<GoalScenarioRelation> GoalScenarioRelations { get; set; }
        public DbSet<ProjectScenario> ProjectScenarios { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; } 
        public DbSet<TaskDailyTracker> DailyTrackers { get; set; } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
