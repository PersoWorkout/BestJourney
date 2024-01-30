using Application.Interfaces.Journeys;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JourneyRepository : IJourneyRepository
    {

        public Task<IEnumerable<Journey>> GetJourneys()
        {
            throw new NotImplementedException();
        }

        public Task Create(Journey journey)
        {
            throw new NotImplementedException();
        }

        public Task<Journey?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges(Journey journey)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Journey journey)
        {
            throw new NotImplementedException();
        }
    }
}
