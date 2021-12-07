using Common.Models.Responses;
using System;
using System.Threading.Tasks;

namespace Common.Abstractions
{
    public interface IUserGeoNavigationService
    {

        Task<UserCollectionResponse> GetNearResponseAsync(Guid id, float longitude, float latitude);

    }
}
