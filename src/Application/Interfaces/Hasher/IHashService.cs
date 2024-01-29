namespace Application.Interfaces.Hasher
{
    public interface IHashService
    {
        string Hash(string value);
        bool Verify(string value ,string hash);
    }
}
