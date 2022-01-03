using ChatUI.Views;
using Xamarin.Forms;

namespace ChatUI
{
    public partial class App : Application
    {
        public static string User = "User";
        public App()
        {
            InitializeComponent();
            DIContainer.Init();
            MainPage = new AppShell();
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
