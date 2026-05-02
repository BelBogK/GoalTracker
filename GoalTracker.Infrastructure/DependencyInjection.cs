using GoalTracker.Data;
using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
             
            // Repositories
            services.AddScoped<ILifeAreaRepository, LifeAreaRepository>();

            return services;
        }
    }
}
