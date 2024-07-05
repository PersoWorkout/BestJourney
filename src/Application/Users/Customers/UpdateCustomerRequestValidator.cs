using Domain.Users.Requests;
using FluentValidation;

namespace Application.Users.Customers;

public class UpdateCustomerRequestValidator: AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
