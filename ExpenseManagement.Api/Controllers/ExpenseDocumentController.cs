using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseDocumentController : ControllerBase
    {
        private readonly IMediator mediator;
        public ExpenseDocumentController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<ExpenseDocumentResponse>>> GetAll()
        {
            var operation = new GetAllExpenseDocumentsQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<ExpenseDocumentResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetExpenseDocumentByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Post([FromBody] ExpenseDocumentRequest ExpenseDocument)
        {
            var operation = new CreateExpenseDocumentCommand(ExpenseDocument);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<ExpenseDocumentResponse>> Put([FromRoute] int id, [FromBody] ExpenseDocumentRequest ExpenseDocument)
        {
            var operation = new UpdateExpenseDocumentCommand(id, ExpenseDocument);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeleteExpenseDocumentCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
