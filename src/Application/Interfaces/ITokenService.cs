namespace Application.Interfaces
{
    public interface ITokenService
    {
        string Generate();
        string? ConvertToString(string token);
    }
}
