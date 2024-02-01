using Application.Interfaces;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public string Generate()
        {
            var token = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(token);
        }

        public string? ConvertToString(string token)
        {
            var tokenToBase64 = Convert.FromBase64String(token);

            return tokenToBase64?.ToString();
        }
    }
}
