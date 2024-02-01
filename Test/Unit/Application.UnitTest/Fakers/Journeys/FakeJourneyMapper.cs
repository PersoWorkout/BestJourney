using Application.Mappers;
using AutoMapper;

namespace Application.UnitTest.Fakers.Journeys
{
    public static class FakeJourneyMapper
    {
        public static IMapper Create()
        {
            MapperConfiguration configuration = new(config =>
            {
                config.CreateMap<string, Guid>().ConvertUsing(value => new Guid(value));
                config.CreateMap<Guid, string>().ConvertUsing(value => value.ToString());
                config.AddProfile(new JourneyProfile());
            });

            return new Mapper(configuration);
        }
    }
}
