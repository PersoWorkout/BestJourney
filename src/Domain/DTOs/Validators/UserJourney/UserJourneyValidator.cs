using Domain.Utils;

namespace Domain.DTOs.Validators.UserJourney
{
    public class UserJourneyValidator
    {
        public string JourneyId { get; set; }
        public int PeopleNumber { get; set; }

        public bool Validate()
        {
            return JourneyId.IsValid() &&
                PeopleNumber > 0;
        }
    }
}
