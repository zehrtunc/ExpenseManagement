using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;
namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllExpensesQuery : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(long id) : IRequest<ApiResponse<ExpenseResponse>>;
public record CreateExpenseCommand(ExpenseRequest Expense) : IRequest<ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(long id, ExpenseRequest Expense) : IRequest<ApiResponse<ExpenseResponse>>;
public record DeleteExpenseCommand(long id) : IRequest<ApiResponse>;


