using _MeetHoper_ServerDemo.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace _MeetHoper_ServerDemo.EF
{
    internal static class DbContextSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();

                context.Database.EnsureCreated();
                context.SaveChanges();
            }
        }

        public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DevelopConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connection);
                }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    
    }
}
