using Common;
using Common.Abstractions;
using Common.Models.DTOs;
using Common.Models.Responses;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class UserGeoNavigationService : IUserGeoNavigationService
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IDataBaseUserHandler _dataBaseUserHandler;

        public UserGeoNavigationService(IMemoryCache memoryCache, IDataBaseUserHandler dataBaseUserHandler)
        {
            _memoryCache = memoryCache;
            _dataBaseUserHandler = dataBaseUserHandler;
        }

        public async Task<UserCollectionResponse> GetNearResponseAsync(Guid id, float longitude, float latitude)
        {
            if (!await _dataBaseUserHandler.IsEntityExistAsync(id))
                throw new Exception(Constants.DoesNotExistText);

            var x = float.TryParse(longitude.ToString("N3"), out var xres) ? xres : 0;
            var y = float.TryParse(longitude.ToString("N3"), out var yres) ? yres : 0;
            _memoryCache.GetOrCreate(id, _ => new Geoposition(x, y));
            throw new NotImplementedException();
        }
    }
}