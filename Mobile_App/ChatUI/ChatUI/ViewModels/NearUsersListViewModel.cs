using ChatUI.Abstractions;
using ChatUI.Helpers;
using ChatUI.Models;
using System;
using System.Collections.ObjectModel;
using System.Timers;

namespace ChatUI.ViewModels
{
    public class NearUsersListViewModel : ViewModelBase
    {
        private readonly IAPIInteraction _aPIWorker;
        private readonly Timer _timer = new Timer(10000);

        public ObservableCollection<UserDataModel> UsersNear { get; set; } = new ObservableCollection<UserDataModel>();
        public UserDataModel UserDataModel { get; set; }

        public NearUsersListViewModel(IAPIInteraction interaction)
        {
            _aPIWorker = interaction;
            _timer.Elapsed += _timer_Elapsed;//test
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var _image = PathHelper.DefaultImageUrl;    
            UsersNear.Add(new UserDataModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Aboba",
                Image = _image
            });
        }
    }
}
