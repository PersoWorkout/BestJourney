using Domain.Auth.Requests.Customers;
using Domain.Utils;
using FluentValidation;

namespace Application.Auth.Validators;

public class RegisterCustomerRequestValidator: AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerRequestValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty();

        RuleFor(x => x.Lastname)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(x => x.IsValidPassword())
            .Equal(x => x.PasswordConfirmation);

        RuleFor(x => x.Phone)
            .NotEmpty();
    }
}
