namespace Domain.DTOs.Responses
{
    public class AuthenticatedResponse(TokenDTO tokenDto)
    {
        public string AccessToken { get; set; } = tokenDto.Token;
        public DateTime ExpiratedAt { get; set; } = tokenDto.ExpiratedAt;
    }
}
