using Domain.Orders;
using Domain.Users;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Journeys;

public class Journey
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatorId { get; set; }
    [JsonIgnore]
    public List<Order> Orders { get; set; } = [];
    [JsonIgnore]
    public User Creator { get; set; }

    public Journey() { }

    public Journey(
        string name,
        string description,
        string country,
        string city,
        decimal price,
        Guid creatorId,
        bool isActive = true) 
    {
        Name = name;
        Description = description;
        Country = country; 
        City = city;
        Price = price;
        IsActive = isActive;
        CreatorId = CreatorId;
    }


    public void Update(
        string name,
        string description,
        string country,
        string city,
        decimal price,
        bool isActive)
    {
        if (name.IsValid()) Name = name;
        if (description.IsValid()) Description = description;
        if (country.IsValid()) Country = country;
        if (city.IsValid()) City = city;
        if (price > 0m) Price = price;
        IsActive = isActive;
        UpdatedAt = DateTime.Now;
    }
}
