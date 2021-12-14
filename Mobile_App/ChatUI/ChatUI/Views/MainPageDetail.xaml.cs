using ChatUI.ViewModels;
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
            Console.WriteLine($"{ userImage }");
            var secondPage = new UserPage(data);
            await Navigation.PushAsync(secondPage);
        }
    }
}