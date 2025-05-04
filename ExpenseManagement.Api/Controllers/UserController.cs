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
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ApiResponse<List<UserResponse>>> GetAll()
        {
            var operation = new GetAllUsersQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ApiResponse<UserResponse>> GetByIdAsync([FromRoute] int id)
        {
            var operation = new GetUserByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest User)
        {
            var operation = new CreateUserCommand(User);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ApiResponse<UserResponse>> Put([FromRoute] int id, [FromBody] UserRequest User)
        {
            var operation = new UpdateUserCommand(id, User);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ApiResponse> Delete([FromRoute] int id)
        {
            var operation = new DeleteUserCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
