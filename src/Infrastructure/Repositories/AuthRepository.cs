using Application.Interfaces.Auth;
using Domain.Auth;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
    public class AuthRepository(
        IDistributedCache distributedCache) : IAuthRepository
    {

        private readonly IDistributedCache _distributedCache = distributedCache;

        public async Task<string?> Get(string token)
        {
            return await _distributedCache.GetStringAsync(token);
        }

        public async Task Set(TokenDTO payload)
        {
            await _distributedCache.SetStringAsync(
                payload.Token,
                payload.UserId,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = payload.ExpiratedAt
                });
        }

        public async Task Delete(string token)
        {
            await _distributedCache.RemoveAsync(token);
        }
    }
}
