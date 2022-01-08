using ChatUI.Abstractions;
using ChatUI.Models;
using ChatUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ChatUI.ViewModels
{
    public class NearUsersListViewModel : ViewModelBase
    {
        private readonly IAPIInteraction _aPIWorker;
        private readonly Timer _timer = new Timer(5000);

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
            UsersNear.Add(new UserDataModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = new Random().Next(0, 100).ToString(),
                Image = string.Empty
            });
        }
    }
}
