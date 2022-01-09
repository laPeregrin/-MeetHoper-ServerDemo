using ChatUI.Abstractions;
using ChatUI.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        private readonly IIOManager _iOManager;

        public AppShell(IIOManager iOManager)
        {
            InitializeComponent();

            _iOManager = iOManager;
            Routing.RegisterRoute("//RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("//NearUsersListPage", typeof(NearUsersListPage));
            Routing.RegisterRoute("//ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("//LogInPage", typeof(LogInPage));
        }

        #region UIHandler

        private async void TapGestureRecognizer_onTapped(object sender, EventArgs e)
        {
            await Current.GoToAsync($"//{nameof(ProfilePage)}");
        }

        #endregion //UIHandler

        public async Task InitStartUpPageAsync()
        {
            var value = _iOManager.ReadAll(PathHelper.UserCredentialFile);
            if (string.IsNullOrEmpty(value))
                await Current.GoToAsync($"//{nameof(LogInPage)}");
        }

        public async Task MoveToRegistrationAsync() =>
            await Current.GoToAsync($"//{nameof(RegistrationPage)}");

        public async Task MoveToGeneralAsync() =>
          await Current.GoToAsync($"//{nameof(MainPageDetail)}");

    }
}