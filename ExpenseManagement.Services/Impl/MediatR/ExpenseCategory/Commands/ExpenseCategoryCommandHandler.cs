using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using ExpenseManagement.Services;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;

namespace Transact.Api.Impl.MediatR;

public class ExpenseCategoryCommandHandler :
     IRequestHandler<CreateExpenseCategoryCommand, ApiResponse<ExpenseCategoryResponse>>,
     IRequestHandler<UpdateExpenseCategoryCommand, ApiResponse<ExpenseCategoryResponse>>,
     IRequestHandler<DeleteExpenseCategoryCommand, ApiResponse>
{
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private readonly IMapper _mapper;

    public ExpenseCategoryCommandHandler(IExpenseCategoryRepository ExpenseCategoryRepository, IMapper mapper)
    {
        _expenseCategoryRepository = ExpenseCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        ExpenseCategoryRequest ExpenseCategoryRequest = request.ExpenseCategory;
        ExpenseCategory ExpenseCategory = _mapper.Map<ExpenseCategory>(ExpenseCategoryRequest);

        await _expenseCategoryRepository.AddAsync(ExpenseCategory);

        ExpenseCategoryResponse ExpenseCategoryResponse = _mapper.Map<ExpenseCategoryResponse>(ExpenseCategory);

        return new ApiResponse<ExpenseCategoryResponse>(ExpenseCategoryResponse);
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        ExpenseCategory ExpenseCategoryEntity = await _expenseCategoryRepository.GetByIdAsync(request.id);

        // ExpenseCategory var mı? 
        if (ExpenseCategoryEntity == null)
        {
            return new ApiResponse<ExpenseCategoryResponse>("ExpenseCategory not found");
        }

        ExpenseCategoryResponse ExpenseCategoryResponse = _mapper.Map<ExpenseCategoryResponse>(ExpenseCategoryEntity);

        return new ApiResponse<ExpenseCategoryResponse>(ExpenseCategoryResponse);
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        ExpenseCategory ExpenseCategory = await _expenseCategoryRepository.GetByIdAsync(request.id);

        if (ExpenseCategory == null) return new ApiResponse("ExpenseCategory not found");

        ExpenseCategory.IsActive = false;
        await _expenseCategoryRepository.UpdateAsync(ExpenseCategory);

        return new ApiResponse();
    }
}
