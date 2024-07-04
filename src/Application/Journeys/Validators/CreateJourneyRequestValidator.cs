using Domain.Journeys.Requests;
using FluentValidation;

namespace Application.Journeys.Validators;

public class CreateJourneyRequestValidator: AbstractValidator<CreateJourneyRequest>
{
    public CreateJourneyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.City)
            .NotEmpty();

        RuleFor(x => x.Country)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
