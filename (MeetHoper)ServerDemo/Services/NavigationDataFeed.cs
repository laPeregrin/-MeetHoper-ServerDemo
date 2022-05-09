using Common.Abstractions;
using Common.Models.Responses;
using System;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class NavigationDataFeed
    {
        private readonly IUserGeoNavigationService _navigationService;
        private readonly IIdemposityService<Guid, UserCollectionResponse> _idemposityService;

        public NavigationDataFeed(IUserGeoNavigationService navigationService, IIdemposityService<Guid, UserCollectionResponse> idemposityService)
        {
            _navigationService = navigationService;
            _idemposityService = idemposityService;
        }

        public async Task<UserCollectionResponse> GetNearResponseAsync(Guid id, double longitude, double latitude)
        {
            var task = _navigationService.GetNearResponseAsync(id, new Common.Models.DTOs.Geoposition(longitude, latitude));
            return await _idemposityService.TryAddProcessAsync(id, task);
        }
    }
}
