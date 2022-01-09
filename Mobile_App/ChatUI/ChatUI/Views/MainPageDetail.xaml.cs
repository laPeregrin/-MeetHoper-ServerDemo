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
            var image = "https://cdn.business2community.com/wp-content/uploads/2017/08/blank-profile-picture-973460_640.png";
            BindingContext = image;
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