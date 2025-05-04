using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseCategoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        public ExpenseCategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<ExpenseCategoryResponse>>> GetAll()
        {
            var operation = new GetAllExpenseCategorysQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<ExpenseCategoryResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetExpenseCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<ExpenseCategoryResponse>> Post([FromBody] ExpenseCategoryRequest ExpenseCategory)
        {
            var operation = new CreateExpenseCategoryCommand(ExpenseCategory);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<ExpenseCategoryResponse>> Put([FromRoute] int id, [FromBody] ExpenseCategoryRequest ExpenseCategory)
        {
            var operation = new UpdateExpenseCategoryCommand(id, ExpenseCategory);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeleteExpenseCategoryCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
