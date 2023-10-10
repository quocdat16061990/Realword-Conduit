using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatDotnetConduit.Infrasturcture.Extensions
{
    public static class ConfigDbExtension
    {
        public static void ConfigDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")).EnableSensitiveDataLogging());
        }

        public static void ConfigRunMigration(this IServiceCollection services)
        {
            var mainDbContext = services.BuildServiceProvider().GetRequiredService<MainDbContext>();
            if(mainDbContext != null) 
            {
                mainDbContext.Database.Migrate();
            }
        }
    }
}
