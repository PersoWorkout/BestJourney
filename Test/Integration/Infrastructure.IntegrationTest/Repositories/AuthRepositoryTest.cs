using Application.Interfaces.Auth;
using Domain.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Integrationtest.Repositories
{
    public class AuthRepositoryTest:
        BaseIntegrationTest
    {
        protected readonly IAuthRepository _authRepository;
        protected readonly IDistributedCache _distributedCache;

        public AuthRepositoryTest(WebApplicationFactory factory):
            base(factory)
        {
            _authRepository = _scope.ServiceProvider
                .GetRequiredService<IAuthRepository>();

            _distributedCache = _scope.ServiceProvider
                .GetRequiredService<IDistributedCache>();
        }

        [Fact]
        public async void Get_ShouldGetStoredValue_WhenKeyExist()
        {
            //Arrang
            const string key = "1";
            const string value = "stored value";

            await _distributedCache.SetStringAsync(key, value);
            
            //Act
            var result = await _authRepository.Get(key);

            //Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public async void Get_ShouldReturnNullValue_WhenKeyNotExist()
        {
            //Arrang
            const string key = "2";

            //Act
            var result = await _authRepository.Get(key);

            //Assert
            Assert.True(string.IsNullOrEmpty(result));
        }

        [Fact]
        public async void Set_ShouldStoreThePayloadInRedis()
        {
            //Arrang
            const string key = "2";
            const string value = "1";

            var payload = new TokenDTO(key, value);

            //Act
            await _authRepository.Set(payload);

            //Assert
            var result = await _distributedCache
                .GetStringAsync(key);

            Assert.Equal(value, result);
        }

        [Fact]
        public async void Delete_ShouldDeleteStoredValueByKey()
        {
            //Arrang
            const string key = "2";
            const string value = "1";

            await _distributedCache.SetStringAsync(key, value);

            //Act
            await _authRepository.Delete(key);

            //Assert
            var result = await _distributedCache
                .GetStringAsync(key);

            Assert.True(string.IsNullOrEmpty(result));
        }
    }
}
