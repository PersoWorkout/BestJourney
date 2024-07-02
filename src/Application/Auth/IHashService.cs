namespace Application.Auth;

public interface IHashService
{
    string Hash(string value);
    bool Verify(string value, string hash);
}
