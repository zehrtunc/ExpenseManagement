using MediatR;
using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Services.Impl.MediatR;

public class ExpenseCategoryQueryHandler :
    IRequestHandler<GetAllExpenseCategorysQuery, ApiResponse<List<ExpenseCategoryResponse>>>,
    IRequestHandler<GetExpenseCategoryByIdQuery, ApiResponse<ExpenseCategoryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;

    public ExpenseCategoryQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
    {
        _mapper = mapper;
        _expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetAllExpenseCategorysQuery request, CancellationToken cancellationToken)
    {
        List<ExpenseCategory> ExpenseCategorys = await _expenseCategoryRepository.WhereAsync(x => x.IsActive);

        List<ExpenseCategoryResponse> ExpenseCategorysResponse = _mapper.Map<List<ExpenseCategoryResponse>>(ExpenseCategorys);

        return new ApiResponse<List<ExpenseCategoryResponse>>(ExpenseCategorysResponse);
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        ExpenseCategory ExpenseCategory = await _expenseCategoryRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (ExpenseCategory == null)
        {
            return new ApiResponse<ExpenseCategoryResponse>("ExpenseCategory not found");
        }

        ExpenseCategoryResponse ExpenseCategoryResponse = _mapper.Map<ExpenseCategoryResponse>(ExpenseCategory);

        return new ApiResponse<ExpenseCategoryResponse>(ExpenseCategoryResponse);
    }
}
