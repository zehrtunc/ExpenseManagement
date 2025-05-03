using MediatR;

using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;
namespace ExpenseManagement.Services.Impl.MediatR;

public class BankAccountQueryHandler :
    IRequestHandler<GetAllBankAccountsQuery, ApiResponse<List<BankAccountResponse>>>,
    IRequestHandler<GetBankAccountByIdQuery, ApiResponse<BankAccountResponse>>
{
    private readonly IMapper _mapper;
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankAccountQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
    {
        _mapper = mapper;
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<ApiResponse<List<BankAccountResponse>>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
    {
        List<BankAccount> BankAccounts = await _bankAccountRepository.WhereAsync(x => x.IsActive);

        List<BankAccountResponse> BankAccountsResponse = _mapper.Map<List<BankAccountResponse>>(BankAccounts);

        return new ApiResponse<List<BankAccountResponse>>(BankAccountsResponse);
    }

    public async Task<ApiResponse<BankAccountResponse>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
    {
        BankAccount BankAccount = await _bankAccountRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (BankAccount == null)
        {
            return new ApiResponse<BankAccountResponse>("BankAccount not found");
        }

        BankAccountResponse BankAccountResponse = _mapper.Map<BankAccountResponse>(BankAccount);

        return new ApiResponse<BankAccountResponse>(BankAccountResponse);
    }
}
