using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;
namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllExpenseCategorysQuery : IRequest<ApiResponse<List<ExpenseCategoryResponse>>>;
public record GetExpenseCategoryByIdQuery(long id) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
public record CreateExpenseCategoryCommand(ExpenseCategoryRequest ExpenseCategory) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
public record UpdateExpenseCategoryCommand(long id, ExpenseCategoryRequest ExpenseCategory) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
public record DeleteExpenseCategoryCommand(long id) : IRequest<ApiResponse>;


