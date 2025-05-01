using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class EftTransactionQueryHandler :
    IRequestHandler<GetAllEftTransactionsQuery, ApiResponse<List<EftTransactionResponse>>>,
    IRequestHandler<GetEftTransactionByIdQuery, ApiResponse<EftTransactionResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EftTransactionQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetAllEftTransactionsQuery request, CancellationToken cancellationToken)
    {
        List<EftTransaction> EftTransactions = await _context.EftTransactions.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<EftTransactionResponse> EftTransactionsResponse = _mapper.Map<List<EftTransactionResponse>>(EftTransactions);

        return new ApiResponse<List<EftTransactionResponse>>(EftTransactionsResponse);
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(GetEftTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        EftTransaction EftTransaction = await _context.EftTransactions.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (EftTransaction == null)
        {
            return new ApiResponse<EftTransactionResponse>("EftTransaction not found");
        }

        EftTransactionResponse EftTransactionResponse = _mapper.Map<EftTransactionResponse>(EftTransaction);

        return new ApiResponse<EftTransactionResponse>(EftTransactionResponse);
    }
}
