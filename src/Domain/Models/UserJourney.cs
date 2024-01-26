using Domain.Interfaces;

namespace Domain.Models
{
    public class UserJourney: IUserJourney
    {
        public Guid UserId { get; set; }
        public Guid JourneyId { get; set; }
        public int PeopleNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public Journey Journey { get; set; }
    }
}
