using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class AccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
{
    public AccountTransactionValidator()
    {
        When(at => !string.IsNullOrEmpty(at.Description), () => RuleFor(at => at.Description).MaximumLength(100));
        RuleFor(at => at.CreditAmount).PrecisionScale(16, 4, true);
        RuleFor(at => at.DebitAmount).PrecisionScale(16, 4, true);
        RuleFor(at => at.TransferType).NotEmpty().MaximumLength(50);
    }

}

public class CreateAccountTransactionCommandValidator : AbstractValidator<CreateAccountTransactionCommand>
{
    public CreateAccountTransactionCommandValidator()
    {
        RuleFor(v => v.AccountTransaction).SetValidator(new AccountTransactionValidator());
    }
}

public class UpdateAccountTransactionCommandValidator : AbstractValidator<UpdateAccountTransactionCommand>
{
    public UpdateAccountTransactionCommandValidator()
    {
        RuleFor(v => v.AccountTransaction).SetValidator(new AccountTransactionValidator());
    }
}