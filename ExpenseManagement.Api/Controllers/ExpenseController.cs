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
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator mediator;
        public ExpenseController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<ExpenseResponse>>> GetAll()
        {
            var operation = new GetAllExpensesQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<ExpenseResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetExpenseByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<ExpenseResponse>> Post([FromBody] ExpenseRequest Expense)
        {
            var operation = new CreateExpenseCommand(Expense);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<ExpenseResponse>> Put([FromRoute] int id, [FromBody] ExpenseRequest Expense)
        {
            var operation = new UpdateExpenseCommand(id, Expense);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeleteExpenseCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
