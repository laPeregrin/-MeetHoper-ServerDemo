using System;
using _MeetHoper_ServerDemo.EF.Contexts;

public static class DbContextSeed
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }

    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("OpenSeaConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connection);
                //options.LogTo(s => Debug.WriteLine(s));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
    }
}
