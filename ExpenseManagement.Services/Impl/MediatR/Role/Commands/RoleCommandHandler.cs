using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using ExpenseManagement.Services;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;

namespace Transact.Api.Impl.MediatR;

public class RoleCommandHandler :
     IRequestHandler<CreateRoleCommand, ApiResponse<RoleResponse>>,
     IRequestHandler<UpdateRoleCommand, ApiResponse<RoleResponse>>,
     IRequestHandler<DeleteRoleCommand, ApiResponse>
{
    private readonly IRoleRepository _RoleRepository;
    private readonly IMapper _mapper;

    public RoleCommandHandler(IRoleRepository RoleRepository, IMapper mapper)
    {
        _RoleRepository = RoleRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        RoleRequest RoleRequest = request.Role;
        Role Role = _mapper.Map<Role>(RoleRequest);

        await _RoleRepository.AddAsync(Role);

        RoleResponse RoleResponse = _mapper.Map<RoleResponse>(Role);

        return new ApiResponse<RoleResponse>(RoleResponse);
    }

    public async Task<ApiResponse<RoleResponse>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role RoleEntity = await _RoleRepository.GetByIdAsync(request.id);

        // Role var mı? 
        if (RoleEntity == null)
        {
            return new ApiResponse<RoleResponse>("Role not found");
        }

        RoleResponse RoleResponse = _mapper.Map<RoleResponse>(RoleEntity);

        return new ApiResponse<RoleResponse>(RoleResponse);
    }

    public async Task<ApiResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role Role = await _RoleRepository.GetByIdAsync(request.id);

        if (Role == null) return new ApiResponse("Role not found");

        Role.IsActive = false;
        await _RoleRepository.UpdateAsync(Role);

        return new ApiResponse();
    }
}
