using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;
using ExpenseManagement.Services;
using ExpenseManagement.Services.Impl.MediatR;
using MediatR;

namespace Transact.Api.Impl.MediatR;

public class UserCommandHandler :
     IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
     IRequestHandler<UpdateUserCommand, ApiResponse<UserResponse>>,
     IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly IUserRepository _UserRepository;
    private readonly IMapper _mapper;

    public UserCommandHandler(IUserRepository UserRepository, IMapper mapper)
    {
        _UserRepository = UserRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        UserRequest UserRequest = request.User;
        User User = _mapper.Map<User>(UserRequest);

        await _UserRepository.AddAsync(User);

        UserResponse UserResponse = _mapper.Map<UserResponse>(User);

        return new ApiResponse<UserResponse>(UserResponse);
    }

    public async Task<ApiResponse<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User UserEntity = await _UserRepository.GetByIdAsync(request.id);

        // User var mı? 
        if (UserEntity == null)
        {
            return new ApiResponse<UserResponse>("User not found");
        }

        UserResponse UserResponse = _mapper.Map<UserResponse>(UserEntity);

        return new ApiResponse<UserResponse>(UserResponse);
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User User = await _UserRepository.GetByIdAsync(request.id);

        if (User == null) return new ApiResponse("User not found");

        User.IsActive = false;
        await _UserRepository.UpdateAsync(User);

        return new ApiResponse();
    }
}
