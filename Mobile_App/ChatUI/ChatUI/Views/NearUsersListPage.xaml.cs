using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearUsersListPage : ContentPage
    {
        public NearUsersListPage()
        {
            InitializeComponent();
            BindingContext = DIContainer.NearUsersListViewModel;
        }

    }
}
