using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using FluentValidation;

namespace ExpenseManagement.Services.Impl.Validator;

public class ExpenseValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseValidator()
    {
    }
}

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
    }
}

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
    {
    }
}
