using HashPassword;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class PasswordHasher
    {

        private readonly string _salt;

        public PasswordHasher(IConfiguration configuration)
        {
            _salt = configuration.GetSection("Salt").Value;
        }

        public async Task<string> CryptLineAsync(string line) =>
           await Hash.CreateAsync(line, _salt, ByteRange.byte256);

        public async Task<bool> CompareLinesAsync(string password, string hashData) =>
           await Hash.ValidateAsync(password, _salt, hashData, ByteRange.byte256);

    }
}
