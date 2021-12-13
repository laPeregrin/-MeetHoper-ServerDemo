using ChatUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatUI.ViewModels
{
    public class UserPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User user;

        public UserPageViewModel()
        {
            user = new User();
        }
        public string Name
        {
            get { return user.Name; }
            set
            {
                user.Name = value;
                OnPropertyChanged("Title");
            }
        }

        public string ImageSource
        {
            get { return user.Image; }
            set
            {
                user.Image = value;
                OnPropertyChanged("Image");
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
