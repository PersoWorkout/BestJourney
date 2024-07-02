using Domain.Abstractions;

namespace Domain.Journeys;

public class JourneyError
{
    public static readonly Error InvalidPayload = new("Journey.InvalidPayload", "The payload is invalid");
    public static Error NotFound(string id) => new("Journey.NotFound", $"The journey with id = {id} was not found");
}
