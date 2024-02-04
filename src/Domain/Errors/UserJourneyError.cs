using Domain.Abstractions;

namespace Domain.Errors
{
    public class UserJourneyError
    {
        public static readonly Error InvalidPayload = new("UserJourney.InvalidPayload", "The payload is invalid");
        public static readonly Error UserNotFound = new("Users.NotFound", "The user was not found");
        public static readonly Error JourneyNotFound = new("Users.NotFound", "The user was not found");
        public static readonly Error JourneyPaied = new("UserJourney.IsPaied", "An order already exist for this journey and you can't update them because it's paied");
        public static readonly Error PaymentError = new("UserJourney.PaymentError", "An error occured during payment");
    }
}
