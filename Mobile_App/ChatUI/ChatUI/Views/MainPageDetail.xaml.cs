using ChatUI.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            var data = new UserPageViewModel
            {
                Name = $"{userName.Text}",
                ImageSource = $"{ userImage.Source }"
            };
            Console.WriteLine($"{ userImage.Source }");
            var secondPage = new UserPage();
            await Navigation.PushAsync(secondPage);
        }
    }
}