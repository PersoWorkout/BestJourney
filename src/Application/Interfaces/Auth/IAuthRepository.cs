using Domain.DTOs;

namespace Application.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<TokenDTO?> Get(string token);
        Task Set(TokenDTO payload);
        Task Delete(string token);
    }
}
