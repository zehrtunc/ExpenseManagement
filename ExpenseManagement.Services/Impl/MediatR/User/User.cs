using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;
namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllUsersQuery : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(long id) : IRequest<ApiResponse<UserResponse>>;
public record CreateUserCommand(UserRequest User) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(long id, UserRequest User) : IRequest<ApiResponse<UserResponse>>;
public record DeleteUserCommand(long id) : IRequest<ApiResponse>;


