using MediatR;

using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;
namespace ExpenseManagement.Services.Impl.MediatR;

public class ExpenseDocumentQueryHandler :
    IRequestHandler<GetAllExpenseDocumentsQuery, ApiResponse<List<ExpenseDocumentResponse>>>,
    IRequestHandler<GetExpenseDocumentByIdQuery, ApiResponse<ExpenseDocumentResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseDocumentRepository _expenseDocumentRepository;

    public ExpenseDocumentQueryHandler(IExpenseDocumentRepository expenseDocumentRepository, IMapper mapper)
    {
        _mapper = mapper;
        _expenseDocumentRepository = expenseDocumentRepository;
    }

    public async Task<ApiResponse<List<ExpenseDocumentResponse>>> Handle(GetAllExpenseDocumentsQuery request, CancellationToken cancellationToken)
    {
        List<ExpenseDocument> ExpenseDocuments = await _expenseDocumentRepository.WhereAsync(x => x.IsActive);

        List<ExpenseDocumentResponse> ExpenseDocumentsResponse = _mapper.Map<List<ExpenseDocumentResponse>>(ExpenseDocuments);

        return new ApiResponse<List<ExpenseDocumentResponse>>(ExpenseDocumentsResponse);
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(GetExpenseDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        ExpenseDocument ExpenseDocument = await _expenseDocumentRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (ExpenseDocument == null)
        {
            return new ApiResponse<ExpenseDocumentResponse>("ExpenseDocument not found");
        }

        ExpenseDocumentResponse ExpenseDocumentResponse = _mapper.Map<ExpenseDocumentResponse>(ExpenseDocument);

        return new ApiResponse<ExpenseDocumentResponse>(ExpenseDocumentResponse);
    }
}
