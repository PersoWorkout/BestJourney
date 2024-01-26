namespace Domain.Interfaces
{
    public interface IUserJourney
    {
        Guid UserId { get; }
        Guid JourneyId { get; }
        int PeopleNumber { get; }
        DateTime CreatedAt { get; }
    }
}
