using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class EftTransactionCommandHandler :
     IRequestHandler<CreateEftTransactionCommand, ApiResponse<EftTransactionResponse>>,
     IRequestHandler<UpdateEftTransactionCommand, ApiResponse<EftTransactionResponse>>,
     IRequestHandler<DeleteEftTransactionCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EftTransactionCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(CreateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        EftTransactionRequest EftTransactionRequest = request.EftTransaction;
        EftTransaction EftTransaction = _mapper.Map<EftTransaction>(EftTransactionRequest);
        EftTransaction.InsertDate = DateTime.Now;
        EftTransaction.InsertUser = "admin";
        EftTransaction.IsActive = true;
        EftTransaction.TransactionDate = DateTime.Now;
        EftTransaction.ReferenceNumber = Guid.NewGuid().ToString();
        EftTransaction.FeeAmount = request.EftTransaction.Amount * 0.02m;


        await _context.EftTransactions.AddAsync(EftTransaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        EftTransactionResponse EftTransactionResponse = _mapper.Map<EftTransactionResponse>(EftTransaction);

        return new ApiResponse<EftTransactionResponse>(EftTransactionResponse);
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(UpdateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        EftTransaction EftTransactionEntity = await _context.EftTransactions.FindAsync(request.id, cancellationToken);


        if (EftTransactionEntity == null)
        {
            return new ApiResponse<EftTransactionResponse>("EftTransaction not found");
        }

        EftTransactionEntity.FromAccountId = request.EftTransaction.FromAccountId;
        EftTransactionEntity.ReceiverIban = request.EftTransaction.ReceiverIban;
        EftTransactionEntity.ReceiverName = request.EftTransaction.ReceiverName;
        EftTransactionEntity.Description = request.EftTransaction.Description;
        EftTransactionEntity.Amount = request.EftTransaction.Amount;
        EftTransactionEntity.TransactionDate = DateTime.Now;
        EftTransactionEntity.PaymentCategoryCode = request.EftTransaction.PaymentCategoryCode;
        EftTransactionEntity.FeeAmount = request.EftTransaction.Amount * 0.02m;
        EftTransactionEntity.UpdateUser = "admin";
        EftTransactionEntity.UpdateDate = DateTime.Now;



        await _context.SaveChangesAsync(cancellationToken);


        EftTransactionResponse EftTransactionResponse = _mapper.Map<EftTransactionResponse>(EftTransactionEntity);

        return new ApiResponse<EftTransactionResponse>(EftTransactionResponse);
    }

    public async Task<ApiResponse> Handle(DeleteEftTransactionCommand request, CancellationToken cancellationToken)
    {
        EftTransaction EftTransaction = await _context.EftTransactions.FindAsync(request.id, cancellationToken);

        if (EftTransaction == null) return new ApiResponse("EftTransaction not found");

        EftTransaction.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
