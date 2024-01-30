using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    price: 650m)
            );
        }
    }
}
