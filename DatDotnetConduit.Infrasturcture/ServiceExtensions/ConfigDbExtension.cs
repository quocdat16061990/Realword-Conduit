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
    public static class ConfigureDbExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        public static void ConfigureMigration(this IServiceCollection services)
        {
            var mainDbContext = services.BuildServiceProvider().GetRequiredService<MainDbContext>();
            if (mainDbContext is not null)
            {
                mainDbContext.Database.Migrate();
            }
        }
    }
}
