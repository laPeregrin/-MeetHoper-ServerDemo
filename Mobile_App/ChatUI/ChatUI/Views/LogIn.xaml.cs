using System;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        private const string ForwardAnimation = "forward";
        private const string BackrwardAnimation = "backward";

        public LogInPage()
        {
            InitializeComponent();
            BindingContext = DIContainer.LoginViewModel;
            AnimateBackground();
        }

        private async void AnimateBackground()
        {
            Action<double> forward = input => bgGradient.AnchorY = input;
            Action<double> backward = input => bgGradient.AnchorY = input;

            bgGradient.Animate(name: ForwardAnimation, callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
            await Task.Delay(5000);
            bgGradient.Animate(name: BackrwardAnimation, callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
            await Task.Delay(5000);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
        }
    }
}