using ChatUI.Abstractions;
using ChatUI.Models;

namespace ChatUI.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private UserDataModel _user;

        public UserPageViewModel()
        {
            _user = new UserDataModel();
        }

        public string Name
        {
            get { return _user.Name; }
            set
            {
                _user.Name = value;
                RaisePropertyChanged();
            }
        }

        public string ImageSource
        {
            get { return _user.Image; }
            set
            {
                _user.Image = value;
                RaisePropertyChanged();
            }
        }

    }
}
