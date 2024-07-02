namespace Domain.Auth;
public class TokenDTO(
    string token,
    string userId)
{
    public string Token { get; set; } = token;
    public string UserId { get; set; } = userId;
    public DateTime ExpiratedAt { get; set; } = DateTime.Now.AddDays(1);
}
