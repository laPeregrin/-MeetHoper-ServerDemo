using ChatUI.Abstractions;
using ChatUI.Helpers;
using System;

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
            Routing.RegisterRoute("//ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("//LogIn", typeof(LogInPage));

            InitStartUpPage();
        }

        #region UIHandler

        private async void TapGestureRecognizer_onTapped(object sender, EventArgs e)
        {
            await Current.GoToAsync($"//{nameof(ProfilePage)}");
        }

        #endregion //UIHandler

        private void InitStartUpPage()
        {
            var value = _iOManager.ReadAll(PathHelper.UserCredentialFile);
            if (string.IsNullOrEmpty(value))
                Current.GoToAsync($"//{nameof(LogInPage)}");
        }

    }
}