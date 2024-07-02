using Domain.Auth;

namespace Application.Auth;

public interface IAuthRepository
{
    Task<string?> Get(string token);
    Task Set(TokenDTO payload);
    Task Delete(string token);
}
