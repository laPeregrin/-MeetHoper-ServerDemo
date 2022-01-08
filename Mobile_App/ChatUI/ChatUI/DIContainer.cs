using ChatUI.Abstractions;
using ChatUI.Factories;
using ChatUI.Helpers;
using ChatUI.Models;
using ChatUI.Services;
using ChatUI.ViewModels;
using ChatUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ChatUI
{
    public class DIContainer
    {
        public static ServiceProvider Provider { get; private set; }

        public static void Init()
        {
            IServiceCollection services = new ServiceCollection();

            //helpers
            services.AddSingleton<RemoteSettings>();
            services.AddSingleton<IFactory<int, System.Timers.Timer>, TimerFactory>();
            services.AddSingleton<IIOManager, IOManager>();

            //services
            services.AddSingleton<IAPIInteraction, RemoteDataInteractionService>();
            services.AddSingleton<IIOManager, IOManager>();

            //viewModels
            services.AddSingleton<AppShell>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<UserPageViewModel>();
            services.AddTransient<ChatPageViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddSingleton<NearUsersListViewModel>();

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
        public static NearUsersListViewModel NearUsersListViewModel => Provider.GetRequiredService<NearUsersListViewModel>();
        public static AppShell AppShell => Provider.GetRequiredService<AppShell>();
    }
}
