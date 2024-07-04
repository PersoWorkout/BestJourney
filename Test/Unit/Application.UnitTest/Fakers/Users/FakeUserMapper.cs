using Application.Users;
using AutoMapper;

namespace Application.UnitTest.Fakers.Users
{
    public static class FakeUserMapper
    {
        public static IMapper Create()
        {
            MapperConfiguration configuration = new(config =>
                {
                    config.CreateMap<string, Guid>().ConvertUsing(value => new Guid(value));
                    config.CreateMap<Guid, string>().ConvertUsing(value => value.ToString());
                    config.AddProfile(new UserProfile());
                });

            return new Mapper(configuration);
        }
    }
}
