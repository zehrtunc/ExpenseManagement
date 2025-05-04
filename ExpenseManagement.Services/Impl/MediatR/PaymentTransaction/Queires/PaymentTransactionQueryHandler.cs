using MediatR;

using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;
namespace ExpenseManagement.Services.Impl.MediatR;

public class PaymentTransactionQueryHandler :
    IRequestHandler<GetAllPaymentTransactionsQuery, ApiResponse<List<PaymentTransactionResponse>>>,
    IRequestHandler<GetPaymentTransactionByIdQuery, ApiResponse<PaymentTransactionResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPaymentTransactionRepository _PaymentTransactionRepository;

    public PaymentTransactionQueryHandler(IPaymentTransactionRepository PaymentTransactionRepository, IMapper mapper)
    {
        _mapper = mapper;
        _PaymentTransactionRepository = PaymentTransactionRepository;
    }

    public async Task<ApiResponse<List<PaymentTransactionResponse>>> Handle(GetAllPaymentTransactionsQuery request, CancellationToken cancellationToken)
    {
        List<PaymentTransaction> PaymentTransactions = await _PaymentTransactionRepository.WhereAsync(x => x.IsActive);

        List<PaymentTransactionResponse> PaymentTransactionsResponse = _mapper.Map<List<PaymentTransactionResponse>>(PaymentTransactions);

        return new ApiResponse<List<PaymentTransactionResponse>>(PaymentTransactionsResponse);
    }

    public async Task<ApiResponse<PaymentTransactionResponse>> Handle(GetPaymentTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        PaymentTransaction PaymentTransaction = await _PaymentTransactionRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (PaymentTransaction == null)
        {
            return new ApiResponse<PaymentTransactionResponse>("PaymentTransaction not found");
        }

        PaymentTransactionResponse PaymentTransactionResponse = _mapper.Map<PaymentTransactionResponse>(PaymentTransaction);

        return new ApiResponse<PaymentTransactionResponse>(PaymentTransactionResponse);
    }
}
