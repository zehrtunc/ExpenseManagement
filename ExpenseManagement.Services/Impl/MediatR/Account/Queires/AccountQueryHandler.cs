using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountsQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AccountQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        List<Account> Accounts = await _context.Accounts.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<AccountResponse> AccountsResponse = _mapper.Map<List<AccountResponse>>(Accounts);

        return new ApiResponse<List<AccountResponse>>(AccountsResponse);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        Account Account = await _context.Accounts.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (Account == null)
        {
            return new ApiResponse<AccountResponse>("Account not found");
        }

        AccountResponse AccountResponse = _mapper.Map<AccountResponse>(Account);

        return new ApiResponse<AccountResponse>(AccountResponse);
    }
}
