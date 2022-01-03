using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        private const string ForwardAnimation = "forward";
        private const string BackrwardAnimation = "backward";

        private Timer _timer;

        public LogIn()
        {
            InitializeComponent();
            StartAnimation();
        }

        ~LogIn()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private void StartAnimation()
        {
            _timer = DIContainer.TimerFactory.GetValue(5000);

            if (_timer != null)
            {
                _timer.Elapsed += Timer_ElapseAnimation;
                _timer.Start();
            }
        }

        private void Timer_ElapseAnimation(object sender, ElapsedEventArgs e) =>
            AnimateBackground();

        private void AnimateBackground()
        {
            Action<double> forward = input => bgGradient.AnchorY = input;
            Action<double> backward = input => bgGradient.AnchorY = input;

            bgGradient.Animate(name: ForwardAnimation, callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
            bgGradient.Animate(name: BackrwardAnimation, callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPageDetail)}");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
        }
    }
}