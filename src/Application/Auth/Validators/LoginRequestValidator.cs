using Domain.Auth.Requests;
using Domain.Utils;
using FluentValidation;

namespace Application.Auth.Validators;

public class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(x => x.IsValidPassword());
    }
}
