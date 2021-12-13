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
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageFlyoutMenuItem;

            var pageType = Type.GetType($"ChatUI.Views.{item.Title}");
            if (item != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
                IsPresented = false;
            }
        }
    }
}