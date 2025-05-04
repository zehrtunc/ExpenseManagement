using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;

namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllExpenseDocumentsQuery : IRequest<ApiResponse<List<ExpenseDocumentResponse>>>;
public record GetExpenseDocumentByIdQuery(long id) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record CreateExpenseDocumentCommand(ExpenseDocumentRequest ExpenseDocument) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record UpdateExpenseDocumentCommand(long id, ExpenseDocumentRequest ExpenseDocument) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record DeleteExpenseDocumentCommand(long id) : IRequest<ApiResponse>;


