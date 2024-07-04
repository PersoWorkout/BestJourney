using Domain.Auth.Requests.Suppliers;
using Domain.Utils;
using FluentValidation;

namespace Application.Auth.Validators;

public class RegisterSupplierRequestValidator: AbstractValidator<RegisterSupplierRequest>
{
    public RegisterSupplierRequestValidator()
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

        RuleFor(x => x.CompanyName)
            .NotEmpty();

        RuleFor(x => x.WebsiteName)
            .NotEmpty();

        RuleFor(x => x.WebsiteUrl)
            .NotEmpty();
    }
}
