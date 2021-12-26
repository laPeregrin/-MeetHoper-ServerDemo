using _MeetHoper_ServerDemo.Interfaces;
using Common.Abstractions;
using Common.Models.DTOs;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class GeolocationCache : ICashService<Geoposition, User>
    {
        private readonly ConcurrentDictionary<Guid, Geoposition> _geoByUserId;
        private readonly IDataBaseUserHandler _dataBaseUserHandle;

        public GeolocationCache(IDataBaseUserHandler dataBaseUserHandle)
        {
            _geoByUserId = new ConcurrentDictionary<Guid, Geoposition>();
            _dataBaseUserHandle = dataBaseUserHandle;
        }

        public async Task<User[]> GetArrayByFuncIdAsync(Func<Geoposition, bool> func, Guid id)
        {
            if (!_geoByUserId.TryGetValue(id, out var userGeo))
                return Array.Empty<User>();

            var ids = _geoByUserId.Where(geoById => func(geoById.Value)).Select(u => u.Key).ToArray();
            return await _dataBaseUserHandle.GetEntitiesByIdsAsync(ids);
        }

        public Task<Geoposition> GetItemAsync(Guid itemId)  =>
           _geoByUserId.TryGetValue(itemId, out var res) ? Task.FromResult(res) : default;

        public Task<bool> SetRecordAsync(Guid itemId, Geoposition data, TimeSpan? absoluteExpireTime) 
        {
            _geoByUserId[itemId] = data;
            return Task.FromResult(true);
        }
    }
}
