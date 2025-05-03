using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using ExpenseManagement.Services;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;

namespace Transact.Api.Impl.MediatR;

public class ExpenseCommandHandler :
     IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
     IRequestHandler<UpdateExpenseCommand, ApiResponse<ExpenseResponse>>,
     IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
    private readonly IExpenseRepository _ExpenseRepository;
    private readonly IMapper _mapper;

    public ExpenseCommandHandler(IExpenseRepository ExpenseRepository, IMapper mapper)
    {
        _ExpenseRepository = ExpenseRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        ExpenseRequest ExpenseRequest = request.Expense;
        Expense Expense = _mapper.Map<Expense>(ExpenseRequest);

        await _ExpenseRepository.AddAsync(Expense);

        ExpenseResponse ExpenseResponse = _mapper.Map<ExpenseResponse>(Expense);

        return new ApiResponse<ExpenseResponse>(ExpenseResponse);
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense ExpenseEntity = await _ExpenseRepository.GetByIdAsync(request.id);

        // Expense var mı? 
        if (ExpenseEntity == null)
        {
            return new ApiResponse<ExpenseResponse>("Expense not found");
        }

        ExpenseResponse ExpenseResponse = _mapper.Map<ExpenseResponse>(ExpenseEntity);

        return new ApiResponse<ExpenseResponse>(ExpenseResponse);
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense Expense = await _ExpenseRepository.GetByIdAsync(request.id);

        if (Expense == null) return new ApiResponse("Expense not found");

        Expense.IsActive = false;
        await _ExpenseRepository.UpdateAsync(Expense);

        return new ApiResponse();
    }
}
