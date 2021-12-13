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
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            BindingContext = new RegistrationViewModel();
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (userName.Text != null)
            {
                Console.WriteLine($"{userName.Text}");
                Console.WriteLine("Aboba");
            }
            else
            {
                DisplayAlert("Registration Failed", "Enter your details", "Okay", "Cancel");
                Console.WriteLine($"{userName.Text}");
            }
        }
    }
}