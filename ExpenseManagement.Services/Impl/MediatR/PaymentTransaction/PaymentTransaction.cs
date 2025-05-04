using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;
namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllPaymentTransactionsQuery : IRequest<ApiResponse<List<PaymentTransactionResponse>>>;
public record GetPaymentTransactionByIdQuery(long id) : IRequest<ApiResponse<PaymentTransactionResponse>>;
public record CreatePaymentTransactionCommand(PaymentTransactionRequest PaymentTransaction) : IRequest<ApiResponse<PaymentTransactionResponse>>;
public record UpdatePaymentTransactionCommand(long id, PaymentTransactionRequest PaymentTransaction) : IRequest<ApiResponse<PaymentTransactionResponse>>;
public record DeletePaymentTransactionCommand(long id) : IRequest<ApiResponse>;


