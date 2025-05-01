using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class CustomerAddressValidator : AbstractValidator<CustomerAddressRequest>
{
    public CustomerAddressValidator()
    {
        When(ca => !string.IsNullOrEmpty(ca.CountryCode), () => RuleFor(ca => ca.CountryCode).MaximumLength(3).WithMessage("Country code must have maximum 3 digit."));
        When(ca => !string.IsNullOrEmpty(ca.City), () => RuleFor(ca => ca.City).MaximumLength(50).WithMessage("City must have maximum 50 digit."));
        When(ca => !string.IsNullOrEmpty(ca.District), () => RuleFor(ca => ca.District).MaximumLength(50).WithMessage("District must have maximum 50 digit."));
        When(ca => !string.IsNullOrEmpty(ca.Street), () => RuleFor(ca => ca.Street).MaximumLength(50).WithMessage("Street must have maximum 50 digit."));
        When(ca => !string.IsNullOrEmpty(ca.ZipCode), () => RuleFor(ca => ca.ZipCode).MaximumLength(10).WithMessage("ZipCode must have maximum 10 digit."));
        RuleFor(ca => ca.IsDefault).NotEmpty();
    }
}

public class CreateCustomerAddressCommandValidator : AbstractValidator<CreateCustomerAddressCommand>
{
    public CreateCustomerAddressCommandValidator()
    {
        RuleFor(v => v.CustomerAddress).SetValidator(new CustomerAddressValidator());
    }
}

public class UpdateCustomerAddresCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
{
    public UpdateCustomerAddresCommandValidator()
    {
        RuleFor(v => v.CustomerAddress).SetValidator(new CustomerAddressValidator());
    }
}
