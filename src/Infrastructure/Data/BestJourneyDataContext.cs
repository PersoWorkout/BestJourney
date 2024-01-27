using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BestJourneyDataContext(DbContextOptions<BestJourneyDataContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<UserJourney> UsersJourneys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasKey(user => user.Id);
            //modelBuilder.Entity<Journey>().HasKey(journey => journey.Id);
            modelBuilder.Entity<UserJourney>().HasKey(userJourney => new {userJourney.JourneyId, userJourney.UserId});
        }
    }
}
