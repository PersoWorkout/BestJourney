using Application.Interfaces.Journeys;
using Domain.Journeys;

namespace Application.UnitTest.Fakers.Journeys
{
    public class FakeJourneyRepository : IJourneyRepository
    {
        private readonly List<Journey> journeys = [];

        public async Task<IEnumerable<Journey>> GetJourneys()
        {
            return await Task.Run(() =>  journeys);
        }

        public async Task Create(Journey journey)
        {
            await Task.Run(() => journeys.Add(journey));
        }

        public async Task<Journey?> GetById(Guid id)
        {
            return await Task.Run(
                () => journeys.FirstOrDefault(journey => journey.Id == id));
        }

        public async Task Delete(Journey journey)
        {
            var index = await Task.Run(
                () => journeys.FindIndex(item => item.Id == journey.Id));

            journeys.RemoveAt(index);
        }

        public async Task SaveChanges(Journey journey)
        {
            var index = await Task.Run(
                () => journeys.FindIndex(journey => journey.Id == journey.Id));

            journeys[index] = journey;

        }
    }
}
