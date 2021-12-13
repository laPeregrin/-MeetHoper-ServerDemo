using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("//RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("//ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("//LogIn", typeof(LogIn));
        }

        private async void TapGestureRecognizer_onTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
        }
    }
}