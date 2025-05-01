using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class AccountValidator : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleFor(a => a.Name).NotEmpty().Length(3, 20).WithMessage("Name must be between 3 and 20");
        //RuleFor(a => a.IBAN).NotEmpty().MaximumLength(26).WithMessage("IBAN must have maximum 26 digits.");
        //RuleFor(a => a.Balance).NotEmpty().ScalePrecision(2, 18).WithMessage("Balance must have maximum 18 digits in total and 2 digits after decimal point.");
        RuleFor(a => a.CurrencyCode).NotEmpty().MaximumLength(3).WithMessage("CurrencyCode must have maximum 3 digits.");
    }
}

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(v => v.Account).SetValidator(new AccountValidator());
    }
}

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(v => v.Account).SetValidator(new AccountValidator());
    }
}
