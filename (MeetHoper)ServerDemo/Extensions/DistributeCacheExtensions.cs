using Common.Models.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Extensions
{
    public static class DistributeCacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                                                   string itemId,
                                                   T data,
                                                   TimeSpan? absoluteExpireTime,
                                                   TimeSpan? slidingExpireTime)
        {
            var serialized = JsonSerializer.Serialize(data);
            await SetStringAsync(cache, itemId, serialized, absoluteExpireTime, slidingExpireTime);
        }

        public static async Task SetStringAsync(this IDistributedCache cache,
                                                string itemId,
                                                string data,
                                                TimeSpan? absoluteExpireTime,
                                                TimeSpan? slidingExpireTime)
        {
            if (string.IsNullOrEmpty(data))
                return;

            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(300),
                SlidingExpiration = slidingExpireTime
            };

            await cache.SetStringAsync(itemId, data, options);
        }

        public static async Task<T> GetItem<T>(this IDistributedCache cache, string itemId)
        {
            var result = default(T);
            var serialized = await cache.GetStringAsync(itemId);
            if (string.IsNullOrEmpty(serialized))
                return result;

            try
            {
                result = JsonSerializer.Deserialize<T>(serialized);
            }
            catch(Exception) { }

            return result;
        }

    }

}
