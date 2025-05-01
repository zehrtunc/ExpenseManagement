using FluentValidation;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Impl.Validator;

public class EftTransactionValidator : AbstractValidator<EftTransactionRequest>
{
    public EftTransactionValidator()
    {
        RuleFor(et => et.ReceiverIban).NotEmpty().MaximumLength(26).WithMessage("Receiver Iban must have maximum 26 digits");
        RuleFor(et => et.ReceiverName).NotEmpty().MaximumLength(50).WithMessage("Receiver Name must have maximum 50 digit");
        RuleFor(et => et.Description).NotEmpty().MaximumLength(500).WithMessage("Description must have maximum 500 digit");
        RuleFor(et => et.PaymentCategoryCode).NotEmpty().MaximumLength(50).WithMessage("Payment Category Code must have maximum 50 digits");
        //RuleFor(et => et.ReferenceNumber).NotEmpty().MaximumLength(50).WithMessage("Reference Number must have maximum 50 digits");
        RuleFor(et => et.Amount).NotEmpty().PrecisionScale(16, 4, true).WithMessage("Amount must have maximum 16 digits in total and 4 after the decimal.");

    }
}

public class CreateEftTransactionCommandValidator : AbstractValidator<CreateEftTransactionCommand>
{
    public CreateEftTransactionCommandValidator()
    {
        RuleFor(v => v.EftTransaction).SetValidator(new EftTransactionValidator());
    }
}

public class UpdateEftTransactionCommandValidator : AbstractValidator<UpdateEftTransactionCommand>
{
    public UpdateEftTransactionCommandValidator()
    {
        RuleFor(v => v.EftTransaction).SetValidator(new EftTransactionValidator());
    }
}
