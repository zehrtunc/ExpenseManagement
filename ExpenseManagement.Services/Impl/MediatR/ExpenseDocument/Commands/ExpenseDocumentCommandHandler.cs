using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using MediatR;

namespace ExpenseManagement.Services.Impl.MediatR;

public class ExpenseDocumentCommandHandler :
     IRequestHandler<CreateExpenseDocumentCommand, ApiResponse<ExpenseDocumentResponse>>,
     IRequestHandler<UpdateExpenseDocumentCommand, ApiResponse<ExpenseDocumentResponse>>,
     IRequestHandler<DeleteExpenseDocumentCommand, ApiResponse>
{
    private readonly IExpenseDocumentRepository _expenseDocumentRepository;
    private readonly IMapper _mapper;

    public ExpenseDocumentCommandHandler(IExpenseDocumentRepository ExpenseDocumentRepository, IMapper mapper)
    {
        _expenseDocumentRepository = ExpenseDocumentRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(CreateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        ExpenseDocumentRequest ExpenseDocumentRequest = request.ExpenseDocument;
        ExpenseDocument ExpenseDocument = _mapper.Map<ExpenseDocument>(ExpenseDocumentRequest);

        await _expenseDocumentRepository.AddAsync(ExpenseDocument);

        ExpenseDocumentResponse ExpenseDocumentResponse = _mapper.Map<ExpenseDocumentResponse>(ExpenseDocument);

        return new ApiResponse<ExpenseDocumentResponse>(ExpenseDocumentResponse);
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(UpdateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        ExpenseDocument ExpenseDocumentEntity = await _expenseDocumentRepository.GetByIdAsync(request.id);

        // ExpenseDocument var mı? 
        if (ExpenseDocumentEntity == null)
        {
            return new ApiResponse<ExpenseDocumentResponse>("ExpenseDocument not found");
        }

        ExpenseDocumentResponse ExpenseDocumentResponse = _mapper.Map<ExpenseDocumentResponse>(ExpenseDocumentEntity);

        return new ApiResponse<ExpenseDocumentResponse>(ExpenseDocumentResponse);
    }

    public async Task<ApiResponse> Handle(DeleteExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        ExpenseDocument ExpenseDocument = await _expenseDocumentRepository.GetByIdAsync(request.id);

        if (ExpenseDocument == null) return new ApiResponse("ExpenseDocument not found");

        ExpenseDocument.IsActive = false;
        await _expenseDocumentRepository.UpdateAsync(ExpenseDocument);

        return new ApiResponse();
    }
}
