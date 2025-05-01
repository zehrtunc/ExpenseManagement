using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoneyTransferController : ControllerBase
{
    private readonly IMediator _mediator;
    public MoneyTransferController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMoneyTransfersQuery());
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetMoneyTransferByIdQuery(id));
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MoneyTransferRequest MoneyTransferRequest)
    {
        var result = await _mediator.Send(new CreateMoneyTransferCommand(MoneyTransferRequest));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] MoneyTransferRequest MoneyTransferRequest)
    {
        var result = await _mediator.Send(new UpdateMoneyTransferCommand(id, MoneyTransferRequest));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteMoneyTransferCommand(id));
        return Ok(result);
    }
}
