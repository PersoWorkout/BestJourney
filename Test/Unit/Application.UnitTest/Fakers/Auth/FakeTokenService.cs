using Application.Auth;

namespace Application.UnitTest.Fakers.Auth
{
    public class FakeTokenService : ITokenService
    {
        public string? ConvertToString(string token)
        {
            return token;
        }

        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
