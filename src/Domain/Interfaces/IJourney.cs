namespace Domain.Interfaces
{
    public interface IJourney
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        string Country { get; }
        string City { get; }
        decimal Price { get; }
        bool IsActive { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
