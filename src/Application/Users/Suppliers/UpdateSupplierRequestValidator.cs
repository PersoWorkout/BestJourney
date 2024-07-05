using Domain.Users.Requests;
using FluentValidation;

namespace Application.Users.Suppliers;

public class UpdateSupplierRequestValidator: AbstractValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
