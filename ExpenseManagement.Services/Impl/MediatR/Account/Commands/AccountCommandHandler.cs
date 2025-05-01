using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class AccountCommandHandler :
     IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
     IRequestHandler<UpdateAccountCommand, ApiResponse<AccountResponse>>,
     IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AccountCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        AccountRequest AccountRequest = request.Account;
        Account Account = _mapper.Map<Account>(AccountRequest);

        Account.InsertDate = DateTime.Now;
        Account.InsertUser = "admin";
        Account.IsActive = true;
        Account.OpenDate = DateTime.Now;
        Account.CloseDate = DateTime.Now.AddYears(5);
        Account.IBAN = "TR" + new Random().Next(10, 99) + Guid.NewGuid().ToString("N").Substring(0, 22).ToUpper();
        Account.Balance = 0;
        Account.AccountNumber = new Random().Next(1000000, 999999999);

        await _context.Accounts.AddAsync(Account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        AccountResponse AccountResponse = _mapper.Map<AccountResponse>(Account);

        return new ApiResponse<AccountResponse>(AccountResponse);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        Account AccountEntity = await _context.Accounts.FindAsync(request.id, cancellationToken);

        // Account var mı? 
        if (AccountEntity == null)
        {
            return new ApiResponse<AccountResponse>("Account not found");
        }

        //Entity gelen veri olarak güncellenir
        AccountEntity.Name = request.Account.Name;
        AccountEntity.CurrencyCode = request.Account.CurrencyCode;
        AccountEntity.CustomerId = request.Account.CustomerId;
        AccountEntity.UpdateUser = "admin";
        AccountEntity.UpdateDate = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);


        AccountResponse AccountResponse = _mapper.Map<AccountResponse>(AccountEntity);

        return new ApiResponse<AccountResponse>(AccountResponse);
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        Account Account = await _context.Accounts.FindAsync(request.id, cancellationToken);

        if (Account == null) return new ApiResponse("Account not found");

        Account.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
