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
    public partial class UserPage : ContentPage
    {
        public UserPageViewModel model;
        public UserPage(UserPageViewModel m)
        {
            InitializeComponent();

            model = m;
            BindingContext = model;
        }
    }
}