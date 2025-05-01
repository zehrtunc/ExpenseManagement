using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerPhoneController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerPhoneController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomerPhonesQuery());
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetCustomerPhoneByIdQuery(id));
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerPhoneRequest CustomerPhoneRequest)
    {
        var result = await _mediator.Send(new CreateCustomerPhoneCommand(CustomerPhoneRequest));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] CustomerPhoneRequest CustomerPhoneRequest)
    {
        var result = await _mediator.Send(new UpdateCustomerPhoneCommand(id, CustomerPhoneRequest));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteCustomerPhoneCommand(id));
        return Ok(result);
    }
}
