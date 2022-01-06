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
        private bool _loginResult = false;

        public LoginViewModel(IAPIInteraction aPIInteraction)
        {
            _aPIInteraction = aPIInteraction;
        }

        public bool IsVisible { get => _loginResult;
                                set { _loginResult = value; RaisePropertiesChanged(); } }

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
        {
            _loginResult = !await _aPIInteraction.LoginAsync(Email, Password);
        }); 
    }
}
