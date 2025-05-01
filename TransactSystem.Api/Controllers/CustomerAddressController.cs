using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerAddressController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerAddressController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomerAddresssQuery());
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetCustomerAddressByIdQuery(id));
        return Ok(result);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerAddressRequest CustomerAddressRequest)
    {
        var result = await _mediator.Send(new CreateCustomerAddressCommand(CustomerAddressRequest));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] CustomerAddressRequest CustomerAddressRequest)
    {
        var result = await _mediator.Send(new UpdateCustomerAddressCommand(id, CustomerAddressRequest));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteCustomerAddressCommand(id));
        return Ok(result);
    }
}
