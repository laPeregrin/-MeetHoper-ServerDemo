using ChatUI.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DIContainer.Init();
            MainPage = DIContainer.AppShell;
        }

        protected async override void OnStart()
        {
            await InitStartUpAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async Task InitStartUpAsync()
        {
            if (MainPage is AppShell appShell)
                await appShell.InitStartUpPageAsync();
        }
    }
}
