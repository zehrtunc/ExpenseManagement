using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class CustomerPhoneValidator : AbstractValidator<CustomerPhoneRequest>
{
    public CustomerPhoneValidator()
    {
        RuleFor(cp => cp.CountryCode).NotEmpty().MaximumLength(3).WithMessage("Country code must have maximum 3 digit.");
        RuleFor(cp => cp.PhoneNumber).NotEmpty().MaximumLength(12).WithMessage("Country code must have maximum 12 digits.");
        RuleFor(cp => cp.IsDefault).NotEmpty();
    }
}
public class CreateCustomerPhoneCommandValidator : AbstractValidator<CreateCustomerPhoneCommand>
{
    public CreateCustomerPhoneCommandValidator()
    {
        RuleFor(v => v.CustomerPhone).SetValidator(new CustomerPhoneValidator());
    }
}

public class UpdateCustomerPhoneCommandValidator : AbstractValidator<UpdateCustomerPhoneCommand>
{
    public UpdateCustomerPhoneCommandValidator()
    {
        RuleFor(v => v.CustomerPhone).SetValidator(new CustomerPhoneValidator());
    }
}
