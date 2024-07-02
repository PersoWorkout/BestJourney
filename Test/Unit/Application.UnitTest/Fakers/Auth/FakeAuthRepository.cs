using Application.Auth;
using Domain.Auth;

namespace Application.UnitTest.Fakers.Auth
{
    public class FakeAuthRepository : IAuthRepository
    {
        public readonly List<TokenDTO> _tokens = [];

        public async Task Delete(string token)
        {
            var tokenIndex = await Task.Run(() => 
                _tokens
                .FindIndex(
                    t => t.Token == token));

            if(tokenIndex > -1)
            {
                _tokens.RemoveAt(tokenIndex);
            }
        }

        public async Task<string?> Get(string token)
        {
            var tokenValue = await Task.Run(() => 
                _tokens.FirstOrDefault(t => t.Token == token));

            return tokenValue?.UserId;
        }

        public async Task Set(TokenDTO payload)
        {
            await Task.Run(() => _tokens.Add(payload));
        }
    }
}
