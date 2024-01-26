namespace Domain.Interfaces
{
    public interface IUser
    {
        Guid Id { get; }
        string Firstname { get; }
        string Lastname { get; }
        string Email { get; }
        string Password { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
