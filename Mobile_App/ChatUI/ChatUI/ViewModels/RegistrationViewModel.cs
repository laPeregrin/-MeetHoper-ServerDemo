using ChatUI.Abstractions;

namespace ChatUI.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _email;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
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

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
    }
}
