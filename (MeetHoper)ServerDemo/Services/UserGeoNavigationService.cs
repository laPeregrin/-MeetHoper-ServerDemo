using Common;
using Common.Abstractions;
using Common.Models.DTOs;
using Common.Models.Responses;
using System;
using System.Threading.Tasks;
using _MeetHoper_ServerDemo.Interfaces;
using _MeetHoper_ServerDemo.Extensions;

namespace _MeetHoper_ServerDemo.Services
{
    public class UserGeoNavigationService : IUserGeoNavigationService
    {
        #region Constants

        private const double GeoCoef = 0.0065;
        public const int CoordLength= 4;

        #endregion //Constants

        private readonly ICashService<Geoposition, User> _cache;
        private readonly IDataBaseUserHandler _dataBaseUserHandler;

        public UserGeoNavigationService(IDataBaseUserHandler dataBaseUserHandler, ICashService<Geoposition, User> cache)
        {
            _cache = cache;
            _dataBaseUserHandler = dataBaseUserHandler;
        }

        public async Task<UserCollectionResponse> GetNearResponseAsync(Guid id, Geoposition geoposition)
        {
            if (!await _dataBaseUserHandler.IsEntityExistAsync(id))
                throw new Exception(Constants.DoesNotExistText);

            var users = await _cache.GetArrayByFuncIdAsync(g => IsNearLocation(geoposition, g) , id);
            await _cache.SetRecordAsync(id, geoposition, null);

            var response = new UserCollectionResponse();
            response.UsersArray = users.ConvertArrayByArray(u => u.ToResponse());
            return response;
        }

        private bool IsNearLocation(Geoposition geopositionRoot, Geoposition geoposition)
        {
            var xExchange = Math.Round(geopositionRoot.Longitude - geoposition.Longitude, 4);
            var yExchange = Math.Round(geopositionRoot.Latitude - geoposition.Latitude, 4);
            var xCoef = Math.Round(xExchange < 0 ? -xExchange : xExchange, 4);
            var yCoef = Math.Round(yExchange < 0 ? -yExchange : yExchange, 4);
            return xCoef <= GeoCoef && yCoef <= GeoCoef;
        }

    }
}