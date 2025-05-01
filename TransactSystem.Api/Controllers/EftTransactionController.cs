using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EftTransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    public EftTransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEftTransactionsQuery());
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetEftTransactionByIdQuery(id));
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EftTransactionRequest EftTransactionRequest)
    {
        var result = await _mediator.Send(new CreateEftTransactionCommand(EftTransactionRequest));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] EftTransactionRequest EftTransactionRequest)
    {
        var result = await _mediator.Send(new UpdateEftTransactionCommand(id, EftTransactionRequest));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteEftTransactionCommand(id));
        return Ok(result);
    }
}
