using Domain.Models;

namespace Domain.DTOs.Responses
{
    public class UserJourneyResponse
    {
        public Journey Journey { get; set; }
        public int PeopleNumber { get; set; }
        public string Status { get; set; }
    }
}
