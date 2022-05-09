using _MeetHoper_ServerDemo.Interfaces;
using Common.Models.DTOs;
using Common.Models.Responses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class GeolocationCache : ICashService<Geoposition, UserPublicDataResponse>
    {
        private readonly ConcurrentDictionary<Guid, Geoposition> _geoByUserId;
        private readonly DapperUserHelper _dapperUserHelper;

        public GeolocationCache(DapperUserHelper dapperUserHelper)
        {
            _geoByUserId = new ConcurrentDictionary<Guid, Geoposition>();
            _dapperUserHelper = dapperUserHelper;
        }

        public async Task<IEnumerable<UserPublicDataResponse>> GetArrayByFuncIdAsync(Func<Geoposition, bool> func, Guid id)
        {
            if (!_geoByUserId.TryGetValue(id, out var userGeo))
                return Array.Empty<UserPublicDataResponse>();

            var ids = _geoByUserId.Where(geoById => func(geoById.Value)).Select(u => u.Key).ToArray();
            return await _dapperUserHelper.GetFromDBByIds(ids);
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
