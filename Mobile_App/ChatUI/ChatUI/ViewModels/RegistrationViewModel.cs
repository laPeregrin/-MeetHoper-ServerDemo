using ChatUI.Abstractions;
using MvvmHelpers.Commands;
using System.Windows.Input;

namespace ChatUI.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private bool _signUpSuccess;

        private readonly IAPIInteraction _aPIInteraction;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(EnableSignUp));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(EnableSignUp));
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

        public bool EnableSignUp => _username.Length > 1 &&
                                    _password.Length > 8 &&
                                    _email.Length > 5 &&
                                    _email.Contains("@");

        public bool SignUpFailed
        {
            get { return _signUpSuccess; }
            set { _signUpSuccess = value; RaisePropertiesChanged(); }
        }

        public RegistrationViewModel(IAPIInteraction aPIInteraction)
        {
            _aPIInteraction = aPIInteraction;
        }

        public ICommand SignUpCmd => new AsyncCommand(async () =>
        {
            SignUpFailed = !await _aPIInteraction.Registration(Username, Password, Email);
            if (!SignUpFailed)
                await DIContainer.AppShell.MoveToListAsync();
        });

    }
}
