using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerRequest customerRequest)
    {
        var result = await _mediator.Send(new CreateCustomerCommand(customerRequest));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] CustomerRequest customerRequest)
    {
        var result = await _mediator.Send(new UpdateCustomerCommand(id, customerRequest));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand(id));
        return Ok(result);
    }
}
