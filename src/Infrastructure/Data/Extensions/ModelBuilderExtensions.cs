using Domain.Journeys;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        string Password = BCrypt.Net.BCrypt.HashPassword("Password123!");
        User customer = new("Tom", "Doe", "customer@example.com", Password, "0601010101");
        User supplier = new("John", "Doe", "supplier@example.com", Password, "0602020202", "Trivago", "Trivago.fr", "https://www.trivago.fr");

        modelBuilder.Entity<User>().HasData(
            [customer, supplier]
        );

        modelBuilder.Entity<Journey>().HasData(
            new Journey(
                name: "Discover Istanbul",
                description: "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world",
                country: "Turkey",
                city: "Istanbul",
                price: 650m,
                supplier.Id),
            new Journey(
                name: "Travel to Alanya",
                description: "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.",
                country: "Turkey",
                city: "Alanya",
                price: 400m,
                supplier.Id)
        );
    }
}
