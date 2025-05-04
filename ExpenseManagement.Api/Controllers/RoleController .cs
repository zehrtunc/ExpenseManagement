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
    public class RoleController : ControllerBase
    {
        private readonly IMediator mediator;
        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<RoleResponse>>> GetAll()
        {
            var operation = new GetAllRolesQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<RoleResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetRoleByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<RoleResponse>> Post([FromBody] RoleRequest Role)
        {
            var operation = new CreateRoleCommand(Role);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<RoleResponse>> Put([FromRoute] int id, [FromBody] RoleRequest Role)
        {
            var operation = new UpdateRoleCommand(id, Role);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeleteRoleCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
