using Common.Abstractions;

namespace Common.Models.Requests
{
    public class AuthenticationRequest<TClient, TToken> : IAuthenticationRequest<TClient, TToken>
    {
        public TClient Client { get; set; }
        public TToken RefreshToken { get; set; }
    }
}
