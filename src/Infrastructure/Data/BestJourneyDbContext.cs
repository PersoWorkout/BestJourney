using Domain.Journeys;
using Domain.Orders;
using Domain.Users;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class BestJourneyDbContext(DbContextOptions<BestJourneyDbContext> options):
    DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Journey> Journeys { get; set; }
    public DbSet<Order> Orders{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => new { x.Email, x.Role })
            .IsUnique();

        modelBuilder.Entity<Journey>()
            .HasOne(x => x.Creator)
            .WithMany(x => x.Journeys)
            .HasForeignKey(x => x.CreatorId);
            

        modelBuilder.Seed();
    }
}
