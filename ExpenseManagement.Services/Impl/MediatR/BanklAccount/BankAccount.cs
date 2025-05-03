using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;
namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllBankAccountsQuery : IRequest<ApiResponse<List<BankAccountResponse>>>;
public record GetBankAccountByIdQuery(long id) : IRequest<ApiResponse<BankAccountResponse>>;
public record CreateBankAccountCommand(BankAccountRequest BankAccount) : IRequest<ApiResponse<BankAccountResponse>>;
public record UpdateBankAccountCommand(long id, BankAccountRequest BankAccount) : IRequest<ApiResponse<BankAccountResponse>>;
public record DeleteBankAccountCommand(long id) : IRequest<ApiResponse>;


