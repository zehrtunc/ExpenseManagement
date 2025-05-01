using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class MoneyTransferQueryHandler :
    IRequestHandler<GetAllMoneyTransfersQuery, ApiResponse<List<MoneyTransferResponse>>>,
    IRequestHandler<GetMoneyTransferByIdQuery, ApiResponse<MoneyTransferResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MoneyTransferQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<MoneyTransferResponse>>> Handle(GetAllMoneyTransfersQuery request, CancellationToken cancellationToken)
    {
        List<MoneyTransfer> MoneyTransfers = await _context.MoneyTransfers.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<MoneyTransferResponse> MoneyTransfersResponse = _mapper.Map<List<MoneyTransferResponse>>(MoneyTransfers);

        return new ApiResponse<List<MoneyTransferResponse>>(MoneyTransfersResponse);
    }

    public async Task<ApiResponse<MoneyTransferResponse>> Handle(GetMoneyTransferByIdQuery request, CancellationToken cancellationToken)
    {
        MoneyTransfer MoneyTransfer = await _context.MoneyTransfers.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (MoneyTransfer == null)
        {
            return new ApiResponse<MoneyTransferResponse>("MoneyTransfer not found");
        }

        MoneyTransferResponse MoneyTransferResponse = _mapper.Map<MoneyTransferResponse>(MoneyTransfer);

        return new ApiResponse<MoneyTransferResponse>(MoneyTransferResponse);
    }
}
