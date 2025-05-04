using MediatR;

using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;
namespace ExpenseManagement.Services.Impl.MediatR;

public class RoleQueryHandler :
    IRequestHandler<GetAllRolesQuery, ApiResponse<List<RoleResponse>>>,
    IRequestHandler<GetRoleByIdQuery, ApiResponse<RoleResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRoleRepository _RoleRepository;

    public RoleQueryHandler(IRoleRepository RoleRepository, IMapper mapper)
    {
        _mapper = mapper;
        _RoleRepository = RoleRepository;
    }

    public async Task<ApiResponse<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        List<Role> Roles = await _RoleRepository.WhereAsync(x => x.IsActive);

        List<RoleResponse> RolesResponse = _mapper.Map<List<RoleResponse>>(Roles);

        return new ApiResponse<List<RoleResponse>>(RolesResponse);
    }

    public async Task<ApiResponse<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Role Role = await _RoleRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (Role == null)
        {
            return new ApiResponse<RoleResponse>("Role not found");
        }

        RoleResponse RoleResponse = _mapper.Map<RoleResponse>(Role);

        return new ApiResponse<RoleResponse>(RoleResponse);
    }
}
