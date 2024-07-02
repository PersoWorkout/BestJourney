using Application.Auth;

namespace Infrastructure.Services
{
    public class HashService: IHashService
    {
        public string Hash(string value)
        {
            try
            {
                return BCrypt.Net.BCrypt.HashPassword(value);
            } catch { return string.Empty; }        
        }

        public bool Verify(string value, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(value, hash);
            } catch {  return false; }
        }
    }
}
