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
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IMediator mediator;
        public PaymentTransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<PaymentTransactionResponse>>> GetAll()
        {
            var operation = new GetAllPaymentTransactionsQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<PaymentTransactionResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetPaymentTransactionByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<PaymentTransactionResponse>> Post([FromBody] PaymentTransactionRequest PaymentTransaction)
        {
            var operation = new CreatePaymentTransactionCommand(PaymentTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<PaymentTransactionResponse>> Put([FromRoute] int id, [FromBody] PaymentTransactionRequest PaymentTransaction)
        {
            var operation = new UpdatePaymentTransactionCommand(id, PaymentTransaction);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeletePaymentTransactionCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
