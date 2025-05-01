using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllAccountsQuery : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountByIdQuery(long id) : IRequest<ApiResponse<AccountResponse>>;
public record CreateAccountCommand(AccountRequest Account) : IRequest<ApiResponse<AccountResponse>>;
public record UpdateAccountCommand(long id, AccountRequest Account) : IRequest<ApiResponse<AccountResponse>>;
public record DeleteAccountCommand(long id) : IRequest<ApiResponse>;


