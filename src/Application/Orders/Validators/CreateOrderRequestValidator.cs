using Domain.Orders.Requests;
using FluentValidation;

namespace Application.Orders.Validators;

public class CreateOrderRequestValidator: AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.JourneyId)
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out var journeyId));

        RuleFor(x => x.ParticipentCount)
            .NotEmpty()
            .GreaterThan(0);
    }
}
