using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class MoneyTransferCommandHandler :
     IRequestHandler<CreateMoneyTransferCommand, ApiResponse<MoneyTransferResponse>>,
     IRequestHandler<UpdateMoneyTransferCommand, ApiResponse<MoneyTransferResponse>>,
     IRequestHandler<DeleteMoneyTransferCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MoneyTransferCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<MoneyTransferResponse>> Handle(CreateMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        MoneyTransferRequest MoneyTransferRequest = request.MoneyTransfer;
        MoneyTransfer MoneyTransfer = _mapper.Map<MoneyTransfer>(MoneyTransferRequest);
        MoneyTransfer.InsertDate = DateTime.Now;
        MoneyTransfer.InsertUser = "admin";
        MoneyTransfer.IsActive = true;
        MoneyTransfer.FeeAmount = request.MoneyTransfer.Amount * 0.02m;
        MoneyTransfer.TransactionDate = DateTime.Now;
        MoneyTransfer.ReferenceNumber = Guid.NewGuid().ToString();

        await _context.MoneyTransfers.AddAsync(MoneyTransfer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        MoneyTransferResponse MoneyTransferResponse = _mapper.Map<MoneyTransferResponse>(MoneyTransfer);

        return new ApiResponse<MoneyTransferResponse>(MoneyTransferResponse);
    }

    public async Task<ApiResponse<MoneyTransferResponse>> Handle(UpdateMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        MoneyTransfer MoneyTransferEntity = await _context.MoneyTransfers.FindAsync(request.id, cancellationToken);

        // MoneyTransfer var mı? 
        if (MoneyTransferEntity == null)
        {
            return new ApiResponse<MoneyTransferResponse>("MoneyTransfer not found");
        }

        //Entity gelen veri olarak güncellenir
        MoneyTransferEntity.FromAccountId = request.MoneyTransfer.FromAccountId;
        MoneyTransferEntity.ToAccountId = request.MoneyTransfer.ToAccountId;
        MoneyTransferEntity.Description = request.MoneyTransfer.Description;
        MoneyTransferEntity.Amount = request.MoneyTransfer.Amount;
        MoneyTransferEntity.FeeAmount = request.MoneyTransfer.Amount * 0.02m;
        MoneyTransferEntity.UpdateUser = "admin";
        MoneyTransferEntity.UpdateDate = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);


        MoneyTransferResponse MoneyTransferResponse = _mapper.Map<MoneyTransferResponse>(MoneyTransferEntity);

        return new ApiResponse<MoneyTransferResponse>(MoneyTransferResponse);
    }

    public async Task<ApiResponse> Handle(DeleteMoneyTransferCommand request, CancellationToken cancellationToken)
    {
        MoneyTransfer MoneyTransfer = await _context.MoneyTransfers.FindAsync(request.id, cancellationToken);

        if (MoneyTransfer == null) return new ApiResponse("MoneyTransfer not found");

        MoneyTransfer.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
