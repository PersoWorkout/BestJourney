using Domain.Models;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BestJourneyDbContext(DbContextOptions<BestJourneyDbContext> options):
        DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<UserJourney> UsersJourneys{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserJourney>()
                .HasKey(e => new { e.UserId, e.JourneyId });

            modelBuilder.Seed();
        }
    }
}
