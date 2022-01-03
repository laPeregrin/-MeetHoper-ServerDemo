using ChatUI.Abstractions;
using ChatUI.Factories;
using ChatUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ChatUI
{
    public class DIContainer
    {
        public static ServiceProvider Provider { get; private set; }

        public static void Init()
        {
            var services = new ServiceCollection();

            //helpers
            services.AddSingleton<IFactory<int, System.Timers.Timer>, TimerFactory>();

            //viewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<UserPageViewModel>();
            services.AddTransient<ChatPageViewModel>();
            services.AddTransient<ProfileViewModel>();

            Provider = services.BuildServiceProvider();
#if DEBUG
            foreach (var item in services)
                Provider.GetRequiredService(item.ServiceType);
#endif
    }

        public static IFactory<int, System.Timers.Timer> TimerFactory => Provider.GetRequiredService<IFactory<int, System.Timers.Timer>>();
        public static LoginViewModel LoginViewModel => Provider.GetRequiredService<LoginViewModel>();
        public static RegistrationViewModel RegistrationViewModel => Provider.GetRequiredService<RegistrationViewModel>();
        public static UserPageViewModel UserPageViewModel => Provider.GetRequiredService<UserPageViewModel>();
        public static ChatPageViewModel ChatPageViewModel => Provider.GetRequiredService<ChatPageViewModel>();
        public static ProfileViewModel ProfileViewModel => Provider.GetRequiredService<ProfileViewModel>();
    }
}
