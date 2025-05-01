using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountTransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountTransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllAccountTransactionsQuery());
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetAccountTransactionByIdQuery(id));
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AccountTransactionRequest AccountTransactionRequest)
    {
        var result = await _mediator.Send(new CreateAccountTransactionCommand(AccountTransactionRequest));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] AccountTransactionRequest AccountTransactionRequest)
    {
        var result = await _mediator.Send(new UpdateAccountTransactionCommand(id, AccountTransactionRequest));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteAccountTransactionCommand(id));
        return Ok(result);
    }
}
