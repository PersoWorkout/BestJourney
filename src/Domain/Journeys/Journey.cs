using Domain.Orders;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Journeys;

public class Journey(
    string name,
    string description,
    string country,
    string city,
    decimal price,
    bool isActive = true)
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string Country { get; set; } = country;
    public string City { get; set; } = city;
    public decimal Price { get; set; } = price;
    public bool IsActive { get; set; } = isActive;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public List<Order> Orders { get; set; } = [];

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
