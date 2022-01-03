using ChatUI.Abstractions;
using ChatUI.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace ChatUI
{
    public static class DIContainer
    {
        public static ServiceProvider Provider { get; private set; }

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IFactory<int, System.Timers.Timer>, TimerFactory>();

            Provider = services.BuildServiceProvider();
#if DEBUG
            foreach (var item in services)
                Provider.GetRequiredService(item.ServiceType);
#endif
    }

        public static IFactory<int, System.Timers.Timer> TimerFactory => Provider.GetRequiredService<IFactory<int, System.Timers.Timer>>();
    }
}
