using ChatUI.Models;
using Common.Models.DTOs;
using Common.Models.Responses;
using System.Threading.Tasks;

namespace ChatUI.Abstractions
{
    public interface IAPIInteraction : IAPIInteractionByToken<SessionToken>
    {

    }

    public interface IAPIInteractionByToken<TToken>
    {
        TToken Token { get; }
        Task<bool> LoginAsync(string userName, string password);

        Task<bool> Registration(string userName, string password, string email);

        Task<UserPublicDataResponse[]> GetUsersByGeolocationAsync(double longitude, double latitude);

        Task<bool> UpdateUserDataAsync(User user);
    }
}
