using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class AccountTransactionQueryHandler :
    IRequestHandler<GetAllAccountTransactionsQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetAccountTransactionByIdQuery, ApiResponse<AccountTransactionResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AccountTransactionQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        List<AccountTransaction> AccountTransactions = await _context.AccountTransactions.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<AccountTransactionResponse> AccountTransactionsResponse = _mapper.Map<List<AccountTransactionResponse>>(AccountTransactions);

        return new ApiResponse<List<AccountTransactionResponse>>(AccountTransactionsResponse);
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        AccountTransaction AccountTransaction = await _context.AccountTransactions.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (AccountTransaction == null)
        {
            return new ApiResponse<AccountTransactionResponse>("AccountTransaction not found");
        }

        AccountTransactionResponse AccountTransactionResponse = _mapper.Map<AccountTransactionResponse>(AccountTransaction);

        return new ApiResponse<AccountTransactionResponse>(AccountTransactionResponse);
    }
}
