using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using FluentValidation;

namespace ExpenseManagement.Services.Impl.Validator;

public class BankAccountValidator : AbstractValidator<BankAccountRequest>
{
    public BankAccountValidator()
    {
    }
}

public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
{
    public CreateBankAccountCommandValidator()
    {
    }
}

public class UpdateBankAccountCommandValidator : AbstractValidator<UpdateBankAccountCommand>
{
    public UpdateBankAccountCommandValidator()
    {
    }
}
