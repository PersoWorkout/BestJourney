using Application.Interfaces.Hasher;

namespace Application.UnitTest.Fakers
{
    public class FakeHashService : IHashService
    {
        public string Hash(string value)
        {
            return value;
        }

        public bool Verify(string value, string hash)
        {
            return value == hash;
        }
    }
}
