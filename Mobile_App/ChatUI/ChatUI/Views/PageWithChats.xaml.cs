using ChatUI.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageWithChats : ContentPage
    {
        public string l_message;
        public PageWithChats()
        {
            InitializeComponent();
            BindingContext = new ChatPageViewModel();
            //BindingContext = l_message;
            //var messages = new ChatPageViewModel().Messages;
            //l_message = messages.Last().Text;
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            var chatPage = new ChatPage();
            await Navigation.PushAsync(chatPage);
        }
    }
}