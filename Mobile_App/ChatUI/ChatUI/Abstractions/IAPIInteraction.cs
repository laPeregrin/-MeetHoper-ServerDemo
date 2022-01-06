using Common.Models.DTOs;
using Common.Models.Responses;
using System.Threading.Tasks;

namespace ChatUI.Abstractions
{
    public interface IAPIInteraction
    {
        Task<bool> LoginAsync(string userName, string password);

        Task<bool> Registration(string userName, string password);

        Task<UserPublicDataResponse[]> GetUsersByGeolocationAsync(double longitude, double latitude);

        Task<User> UpdateUserDataAsync(User user);
    }
}
