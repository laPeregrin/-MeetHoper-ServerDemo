using ChatUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace ChatUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DIContainer.Init();
            MainPage = DIContainer.Provider.GetRequiredService<AppShell>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
