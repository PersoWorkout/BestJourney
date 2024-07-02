using Domain.Models;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
namespace Infrastructure.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>().HasData(
                new Journey(
                    name: "Discover Istanbul",
                    description: "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world",
                    country: "Turkey",
                    city: "Istanbul",
                    price: 650m),
                new Journey(
                    name: "Travel to Alanya",
                    description: "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.",
                    country: "Turkey",
                    city: "Alanya",
                    price: 400m)
            );

            modelBuilder.Entity<User>().HasData(
                new User(
                    firstname: "Yasin",
                    lastname: "Karakus",
                    email: "yasin.karakus@example.com",
                    password: BCrypt.Net.BCrypt.HashPassword("Password123!"))
            );
        }
    }
}
