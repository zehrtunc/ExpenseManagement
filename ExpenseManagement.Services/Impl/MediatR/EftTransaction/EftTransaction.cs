using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllEftTransactionsQuery : IRequest<ApiResponse<List<EftTransactionResponse>>>;
public record GetEftTransactionByIdQuery(long id) : IRequest<ApiResponse<EftTransactionResponse>>;
public record CreateEftTransactionCommand(EftTransactionRequest EftTransaction) : IRequest<ApiResponse<EftTransactionResponse>>;
public record UpdateEftTransactionCommand(long id, EftTransactionRequest EftTransaction) : IRequest<ApiResponse<EftTransactionResponse>>;
public record DeleteEftTransactionCommand(long id) : IRequest<ApiResponse>;


