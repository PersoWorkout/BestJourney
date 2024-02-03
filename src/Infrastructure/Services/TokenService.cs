using Application.Interfaces;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public string Generate()
        {
            return Convert.ToBase64String(
                Encoding.ASCII.GetBytes(Guid.NewGuid().ToString()));
        }
    }
}
