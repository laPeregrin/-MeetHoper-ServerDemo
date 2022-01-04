namespace Common.Abstractions
{
    public interface IAuthenticationRequest<TClient, TToken>
    {
        TClient Client { get; set; }
        TToken RefreshToken { get; set; }
    }
}
