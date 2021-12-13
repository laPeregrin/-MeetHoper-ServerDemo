using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChatUI.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _username;
        private string _password;
        private string _email;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
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

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
    }
}
