using System;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Interfaces
{
    public interface ICashService<TCachedItem, TSpecialItem>
    {
        Task<bool> SetRecordAsync(Guid itemId,
                                  TCachedItem data,
                                  TimeSpan? absoluteExpireTime);

        Task<TCachedItem> GetItemAsync(Guid itemId);

        Task<TSpecialItem[]> GetArrayByFuncIdAsync(Func<TCachedItem, bool> func, Guid id);
    }
}
