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
    public class BankAccountController : ControllerBase
    {
        private readonly IMediator mediator;
        public BankAccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<BankAccountResponse>>> GetAll()
        {
            var operation = new GetAllBankAccountsQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<BankAccountResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetBankAccountByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<BankAccountResponse>> Post([FromBody] BankAccountRequest BankAccount)
        {
            var operation = new CreateBankAccountCommand(BankAccount);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<BankAccountResponse>> Put([FromRoute] int id, [FromBody] BankAccountRequest BankAccount)
        {
            var operation = new UpdateBankAccountCommand(id, BankAccount);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeleteBankAccountCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
