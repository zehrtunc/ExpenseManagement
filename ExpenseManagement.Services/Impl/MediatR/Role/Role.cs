using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using MediatR;
namespace ExpenseManagement.Services.Impl.MediatR;

public record GetAllRolesQuery : IRequest<ApiResponse<List<RoleResponse>>>;
public record GetRoleByIdQuery(long id) : IRequest<ApiResponse<RoleResponse>>;
public record CreateRoleCommand(RoleRequest Role) : IRequest<ApiResponse<RoleResponse>>;
public record UpdateRoleCommand(long id, RoleRequest Role) : IRequest<ApiResponse<RoleResponse>>;
public record DeleteRoleCommand(long id) : IRequest<ApiResponse>;


