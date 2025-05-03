using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using ExpenseManagement.Services;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;

namespace Transact.Api.Impl.MediatR;

public class BankAccountCommandHandler :
     IRequestHandler<CreateBankAccountCommand, ApiResponse<BankAccountResponse>>,
     IRequestHandler<UpdateBankAccountCommand, ApiResponse<BankAccountResponse>>,
     IRequestHandler<DeleteBankAccountCommand, ApiResponse>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IMapper _mapper;

    public BankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
    {
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<BankAccountResponse>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        BankAccountRequest bankAccountRequest = request.BankAccount;
        BankAccount bankAccount = _mapper.Map<BankAccount>(bankAccountRequest);

        await _bankAccountRepository.AddAsync(bankAccount);

        BankAccountResponse BankAccountResponse = _mapper.Map<BankAccountResponse>(bankAccount);

        return new ApiResponse<BankAccountResponse>(BankAccountResponse);
    }

    public async Task<ApiResponse<BankAccountResponse>> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
    {
        BankAccount BankAccountEntity = await _bankAccountRepository.GetByIdAsync(request.id);

        // BankAccount var mı? 
        if (BankAccountEntity == null)
        {
            return new ApiResponse<BankAccountResponse>("BankAccount not found");
        }

        BankAccountResponse BankAccountResponse = _mapper.Map<BankAccountResponse>(BankAccountEntity);

        return new ApiResponse<BankAccountResponse>(BankAccountResponse);
    }

    public async Task<ApiResponse> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
    {
        BankAccount BankAccount = await _bankAccountRepository.GetByIdAsync(request.id);

        if (BankAccount == null) return new ApiResponse("BankAccount not found");

        BankAccount.IsActive = false;
        await _bankAccountRepository.UpdateAsync(BankAccount);

        return new ApiResponse();
    }
}
