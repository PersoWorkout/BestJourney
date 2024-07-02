using Domain.Auth;

namespace Domain.DTOs.Responses;

public class AuthenticatedResponse(TokenDTO tokenDto)
{
    public string Type { get; set; } = "Bearer";
    public string AccessToken { get; set; } = tokenDto.Token;
    public DateTime ExpiratedAt { get; set; } = tokenDto.ExpiratedAt;
}
