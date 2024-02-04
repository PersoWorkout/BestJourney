using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace Domain.Models
{
    public class UserJourney(
        Guid userId,
        Guid journeyId,
        int peopleNumber) : IUserJourney
    {
        public Guid UserId { get; set; } = userId;
        public Guid JourneyId { get; set; } = journeyId;
        public int PeopleNumber { get; set; } = peopleNumber;
        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaied;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; }
        public Journey Journey { get; set; }

        public void Update(
            int? peopleNumber = null,
            PaymentStatus? status = null)
        {
            if(peopleNumber.HasValue) PeopleNumber = peopleNumber.Value;
            if(status.HasValue) Status = status.Value;
            UpdatedAt = DateTime.Now;
        }

    }
}
