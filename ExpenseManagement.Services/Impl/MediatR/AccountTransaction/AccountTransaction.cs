using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllAccountTransactionsQuery : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
public record GetAccountTransactionByIdQuery(long id) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record CreateAccountTransactionCommand(AccountTransactionRequest AccountTransaction) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record UpdateAccountTransactionCommand(long id, AccountTransactionRequest AccountTransaction) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record DeleteAccountTransactionCommand(long id) : IRequest<ApiResponse>;


