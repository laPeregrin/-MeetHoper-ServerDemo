using ChatUI.Abstractions;
using MvvmHelpers.Commands;
using System.Windows.Input;

namespace ChatUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAPIInteraction _aPIInteraction;
        private string _password;
        private string _email;

        private bool _failedLogin;
        private bool _firstLaunch = true;

        public bool FailedLogin
        {
            get => _failedLogin && !_firstLaunch;
            set 
            { 
              _failedLogin = value;
              _firstLaunch = false;
              RaisePropertiesChanged();
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoginCmd => new AsyncCommand(async () =>
            _failedLogin = !await _aPIInteraction.LoginAsync(Email, Password));

        public ICommand RedirectRegistration => new AsyncCommand(async () =>
           await DIContainer.AppShell.MoveToRegistration());


        public LoginViewModel(IAPIInteraction aPIInteraction)
        {
            _aPIInteraction = aPIInteraction;
        }
    }
}
