using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using ExpenseManagement.Services;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;

namespace Transact.Api.Impl.MediatR;

public class PaymentTransactionCommandHandler :
     IRequestHandler<CreatePaymentTransactionCommand, ApiResponse<PaymentTransactionResponse>>,
     IRequestHandler<UpdatePaymentTransactionCommand, ApiResponse<PaymentTransactionResponse>>,
     IRequestHandler<DeletePaymentTransactionCommand, ApiResponse>
{
    private readonly IPaymentTransactionRepository _PaymentTransactionRepository;
    private readonly IMapper _mapper;

    public PaymentTransactionCommandHandler(IPaymentTransactionRepository PaymentTransactionRepository, IMapper mapper)
    {
        _PaymentTransactionRepository = PaymentTransactionRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PaymentTransactionResponse>> Handle(CreatePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        PaymentTransactionRequest PaymentTransactionRequest = request.PaymentTransaction;
        PaymentTransaction PaymentTransaction = _mapper.Map<PaymentTransaction>(PaymentTransactionRequest);

        await _PaymentTransactionRepository.AddAsync(PaymentTransaction);

        PaymentTransactionResponse PaymentTransactionResponse = _mapper.Map<PaymentTransactionResponse>(PaymentTransaction);

        return new ApiResponse<PaymentTransactionResponse>(PaymentTransactionResponse);
    }

    public async Task<ApiResponse<PaymentTransactionResponse>> Handle(UpdatePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        PaymentTransaction PaymentTransactionEntity = await _PaymentTransactionRepository.GetByIdAsync(request.id);

        // PaymentTransaction var mı? 
        if (PaymentTransactionEntity == null)
        {
            return new ApiResponse<PaymentTransactionResponse>("PaymentTransaction not found");
        }

        PaymentTransactionResponse PaymentTransactionResponse = _mapper.Map<PaymentTransactionResponse>(PaymentTransactionEntity);

        return new ApiResponse<PaymentTransactionResponse>(PaymentTransactionResponse);
    }

    public async Task<ApiResponse> Handle(DeletePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        PaymentTransaction PaymentTransaction = await _PaymentTransactionRepository.GetByIdAsync(request.id);

        if (PaymentTransaction == null) return new ApiResponse("PaymentTransaction not found");

        PaymentTransaction.IsActive = false;
        await _PaymentTransactionRepository.UpdateAsync(PaymentTransaction);

        return new ApiResponse();
    }
}
