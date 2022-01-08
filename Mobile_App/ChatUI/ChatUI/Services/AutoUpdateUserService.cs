using ChatUI.Abstractions;
using ChatUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ChatUI.Extensions;
using static DevExpress.XamarinAndroid.Grid.SwipeButtonModel;
using Common.Models.Responses;

namespace ChatUI.Services
{
    public class AutoUpdateUserService
    {
        private readonly System.Timers.Timer _timer; 
        private readonly IAPIInteraction _aPIInteraction;
        private readonly GeolocationRequest _geoRequest = new GeolocationRequest(GeolocationAccuracy.High,
                                                                                 TimeSpan.FromSeconds(30));

        public AutoUpdateUserService(IAPIInteraction aPIInteraction)
        {
            _aPIInteraction = aPIInteraction;
            _timer = new System.Timers.Timer(60000);
        }

        #region Events

        public event EventHandler<UserDataModel[]> UserListEventChange;

        #endregion //Events

        private void InitWorker()
        {
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateUserList()
        {
            var geo = await GetGeolocationAsync();
            var usersResponse = await _aPIInteraction.GetUsersByGeolocationAsync(geo.Longitude, geo.Latitude);
            if (usersResponse != null)
                UserListEventChange?.Invoke(null, usersResponse.MapTo(ConvertToDataModel));
        }

        private async Task<Xamarin.Essentials.Location> GetGeolocationAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            try
            {
                if (location == null)
                    location = await Geolocation.GetLocationAsync(_geoRequest);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return location;
        }

        private UserDataModel ConvertToDataModel(UserPublicDataResponse response) =>
            new UserDataModel()
            {
                Id = response.Id.ToString(),
                Name = response.UserName,
                Image = response.ImageUrl
            };

    }
}
