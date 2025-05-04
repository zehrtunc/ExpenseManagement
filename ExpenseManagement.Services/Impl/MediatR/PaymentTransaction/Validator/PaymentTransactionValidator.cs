using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using FluentValidation;

namespace ExpenseManagement.Services.Impl.Validator;

public class PaymentTransactionValidator : AbstractValidator<PaymentTransactionRequest>
{
    public PaymentTransactionValidator()
    {
    }
}

public class CreatePaymentTransactionCommandValidator : AbstractValidator<CreatePaymentTransactionCommand>
{
    public CreatePaymentTransactionCommandValidator()
    {
    }
}

public class UpdatePaymentTransactionCommandValidator : AbstractValidator<UpdatePaymentTransactionCommand>
{
    public UpdatePaymentTransactionCommandValidator()
    {
    }
}
