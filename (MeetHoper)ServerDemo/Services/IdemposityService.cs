using Common.Abstractions;
using Common.Models.Responses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class IdemposityService : IIdemposityService<Guid, UserCollectionResponse>
    {

        #region Members

        private readonly ConcurrentDictionary<Guid, Task<UserCollectionResponse>> _responseById;

        #endregion //Members

        public IdemposityService()
        {
            _responseById = new ConcurrentDictionary<Guid, Task<UserCollectionResponse>>();
        }

        public async Task<UserCollectionResponse> TryAddProcessAsync(Guid id, Task<UserCollectionResponse> task)
        {
            if (_responseById.TryGetValue(id, out var currentTask))
            {
                return await currentTask;
            }

            _responseById.TryAdd(id, task);
            var result = default(UserCollectionResponse);
            try
            {
                result = await task;
            }
            catch(Exception e)
            {

            }

            _responseById.TryRemove(id, out var _);
            return result;
        }
    }
}
