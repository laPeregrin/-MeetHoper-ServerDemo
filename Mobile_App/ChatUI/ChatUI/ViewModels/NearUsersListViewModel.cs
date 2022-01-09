using ChatUI.Abstractions;
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
            var _image = "https://cdn.business2community.com/wp-content/uploads/2017/08/blank-profile-picture-973460_640.png";
            UsersNear.Add(new UserDataModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Aboba",
                Image = _image
            });
        }
    }
}
