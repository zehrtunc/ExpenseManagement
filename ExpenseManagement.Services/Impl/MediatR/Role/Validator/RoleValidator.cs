using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using FluentValidation;

namespace ExpenseManagement.Services.Impl.Validator;

public class RoleValidator : AbstractValidator<RoleRequest>
{
    public RoleValidator()
    {
    }
}

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
    }
}

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
    }
}
