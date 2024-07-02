using Domain.Journeys;
using Domain.Orders.Enums;
using Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Orders;

public class Order
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    public Guid UserId { get; set; }
    public Guid JourneyId { get; set; }
    public decimal Price { get; set; }
    public int ParticipentCount { get; set; } = 1;
    public PaymentStatus Status { get; set; } = PaymentStatus.Unpaied;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public User User { get; set; }
    [JsonIgnore]
    public Journey Journey { get; set; }

    public void Update(int? participantCount = null, decimal? price = null, PaymentStatus? status = null)
    {
        if (participantCount.HasValue) ParticipentCount = participantCount.Value;
        if (price.HasValue) Price = price.Value;
        if (status.HasValue) Status = status.Value;
        UpdatedAt = DateTime.Now;
    }
}
