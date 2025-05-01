using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class AccountTransactionCommandHandler :
     IRequestHandler<CreateAccountTransactionCommand, ApiResponse<AccountTransactionResponse>>,
     IRequestHandler<UpdateAccountTransactionCommand, ApiResponse<AccountTransactionResponse>>,
     IRequestHandler<DeleteAccountTransactionCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AccountTransactionCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        AccountTransactionRequest AccountTransactionRequest = request.AccountTransaction;
        AccountTransaction AccountTransaction = _mapper.Map<AccountTransaction>(AccountTransactionRequest);
        AccountTransaction.TransactionDate = DateTime.Now;
        AccountTransaction.InsertDate = DateTime.Now;
        AccountTransaction.InsertUser = "admin";
        AccountTransaction.IsActive = true;
        AccountTransaction.ReferenceNumber = Guid.NewGuid().ToString();

        await _context.AccountTransactions.AddAsync(AccountTransaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        AccountTransactionResponse AccountTransactionResponse = _mapper.Map<AccountTransactionResponse>(AccountTransaction);

        return new ApiResponse<AccountTransactionResponse>(AccountTransactionResponse);
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(UpdateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        AccountTransaction AccountTransactionEntity = await _context.AccountTransactions.FindAsync(request.id, cancellationToken);

        if (AccountTransactionEntity == null)
        {
            return new ApiResponse<AccountTransactionResponse>("AccountTransaction not found");
        }

        //Gtirilen data db"dekine set edilir.
        AccountTransactionEntity.UpdateUser = "admin";
        AccountTransactionEntity.UpdateDate = DateTime.Now;
        AccountTransactionEntity.Description = request.AccountTransaction.Description;
        AccountTransactionEntity.DebitAmount = request.AccountTransaction.DebitAmount;
        AccountTransactionEntity.CreditAmount = request.AccountTransaction.CreditAmount;
        AccountTransactionEntity.TransferType = request.AccountTransaction.TransferType;

        await _context.SaveChangesAsync(cancellationToken);


        AccountTransactionResponse AccountTransactionResponse = _mapper.Map<AccountTransactionResponse>(AccountTransactionEntity);

        return new ApiResponse<AccountTransactionResponse>(AccountTransactionResponse);
    }

    public async Task<ApiResponse> Handle(DeleteAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        AccountTransaction AccountTransaction = await _context.AccountTransactions.FindAsync(request.id, cancellationToken);

        if (AccountTransaction == null) return new ApiResponse("AccountTransaction not found");

        AccountTransaction.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
