using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class MoneyTransferValidator : AbstractValidator<MoneyTransferRequest>
{
    public MoneyTransferValidator()
    {
        RuleFor(et => et.Description).NotEmpty().MaximumLength(500).WithMessage("Description must have maximum 500 digit");
        //RuleFor(et => et.ReferenceNumber).NotEmpty().MaximumLength(50).WithMessage("Reference Number must have maximum 50 digits");
        RuleFor(et => et.Amount).NotEmpty().PrecisionScale(16, 4, true).WithMessage("Amount must have maximum 16 digits in total and 4 after the decimal.");
        //RuleFor(et => et.FeeAmount).PrecisionScale(16, 4, true).WithMessage("Amount must have maximum 16 digits in total and 4 after the decimal.");
        //RuleFor(et => et.TransactionDate).NotEmpty();
    }
}

public class CreateMoneyTransferCommandValidator : AbstractValidator<CreateMoneyTransferCommand>
{
    public CreateMoneyTransferCommandValidator()
    {
        RuleFor(v => v.MoneyTransfer).SetValidator(new MoneyTransferValidator());
    }
}

public class UpdateMoneyTransferCommandValidator : AbstractValidator<UpdateMoneyTransferCommand>
{
    public UpdateMoneyTransferCommandValidator()
    {
        RuleFor(v => v.MoneyTransfer).SetValidator(new MoneyTransferValidator());
    }
}
