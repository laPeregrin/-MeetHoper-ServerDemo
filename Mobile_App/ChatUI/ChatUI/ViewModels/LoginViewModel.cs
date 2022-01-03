using ChatUI.Abstractions;

namespace ChatUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _password;
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }
    }
}
