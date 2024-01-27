using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Journey(
        string name,
        string description, 
        string country,
        string city,
        decimal price,
        bool isActive = true): IJourney
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public string Country { get; set; } = country;
        public string City { get; set; } = city;
        public decimal Price { get; set; } = price;
        public bool IsActive { get; set; } = isActive;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<UserJourney> UsersJourneys { get; set; } = [];

        public void Update(
            string name, 
            string description, 
            string country, 
            string city, 
            decimal price, 
            bool isActive)
        {
            Name = name;
            Description = description;
            Country = country;
            City = city;
            Price = price;
            IsActive = isActive;
            UpdatedAt = DateTime.Now;
        }
    }
}
