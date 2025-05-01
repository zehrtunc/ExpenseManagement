using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllMoneyTransfersQuery : IRequest<ApiResponse<List<MoneyTransferResponse>>>;
public record GetMoneyTransferByIdQuery(long id) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record CreateMoneyTransferCommand(MoneyTransferRequest MoneyTransfer) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record UpdateMoneyTransferCommand(long id, MoneyTransferRequest MoneyTransfer) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record DeleteMoneyTransferCommand(long id) : IRequest<ApiResponse>;


