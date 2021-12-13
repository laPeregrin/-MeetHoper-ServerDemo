using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChatUI.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _password;
        private string _email;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
    }
}
