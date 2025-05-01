using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage("Email address is not valid.");
        RuleFor(c => c.FirstName).NotEmpty().Length(3, 20).WithMessage("First Name must be between 3 and 20.");
        When(c => !string.IsNullOrEmpty(c.MiddleName), () => RuleFor(x => x.MiddleName).Length(3, 20).WithMessage("Middle Name must be between 3 and 20."));
        RuleFor(c => c.LastName).NotEmpty().Length(3, 20).WithMessage("Last Name must be between 3 and 20.");
        RuleFor(c => c.IdentityNumber).NotEmpty().Length(11).WithMessage("Identity must contains 11 numbers.");
    }

}

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(v => v.customer).SetValidator(new CustomerValidator());
    }
}

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(v => v.customer).SetValidator(new CustomerValidator());
    }
}