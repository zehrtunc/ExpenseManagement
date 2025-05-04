using MediatR;

using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;
namespace ExpenseManagement.Services.Impl.MediatR;

public class ExpenseQueryHandler :
    IRequestHandler<GetAllExpensesQuery, ApiResponse<List<ExpenseResponse>>>,
    IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _mapper = mapper;
        _expenseRepository = expenseRepository;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        List<Expense> Expenses = await _expenseRepository.WhereAsync(x => x.IsActive);

        List<ExpenseResponse> ExpensesResponse = _mapper.Map<List<ExpenseResponse>>(Expenses);

        return new ApiResponse<List<ExpenseResponse>>(ExpensesResponse);
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        Expense Expense = await _expenseRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (Expense == null)
        {
            return new ApiResponse<ExpenseResponse>("Expense not found");
        }

        ExpenseResponse ExpenseResponse = _mapper.Map<ExpenseResponse>(Expense);

        return new ApiResponse<ExpenseResponse>(ExpenseResponse);
    }
}
