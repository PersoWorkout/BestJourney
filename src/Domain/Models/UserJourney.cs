using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Domain.Models
{
    public class UserJourney(
        Guid userId,
        Guid journeyId,
        int peopleNumber): IUserJourney
    {
        public Guid UserId { get; set; } = userId;
        public Guid JourneyId { get; set; } = journeyId;
        public int PeopleNumber { get; set; } = peopleNumber;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; }
        public Journey Journey { get; set; }

        public void Update(
            int peopleNumber,
            PaymentStatus status)
        {
            PeopleNumber = peopleNumber;
            Status = status;
            UpdatedAt = DateTime.Now;
        }
    }
}
