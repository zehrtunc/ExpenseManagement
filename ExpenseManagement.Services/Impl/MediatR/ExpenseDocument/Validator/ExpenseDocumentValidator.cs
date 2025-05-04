using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using FluentValidation;

namespace ExpenseManagement.Services.Impl.Validator;

public class ExpenseDocumentValidator : AbstractValidator<ExpenseDocumentRequest>
{
    public ExpenseDocumentValidator()
    {
    }
}

public class CreateExpenseDocumentCommandValidator : AbstractValidator<CreateExpenseDocumentCommand>
{
    public CreateExpenseDocumentCommandValidator()
    {
    }
}

public class UpdateExpenseDocumentCommandValidator : AbstractValidator<UpdateExpenseDocumentCommand>
{
    public UpdateExpenseDocumentCommandValidator()
    {
    }
}
