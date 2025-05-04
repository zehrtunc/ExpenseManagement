using MediatR;

using AutoMapper;
using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.Base.Entities;
namespace ExpenseManagement.Services.Impl.MediatR;

public class UserQueryHandler :
    IRequestHandler<GetAllUsersQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _UserRepository;

    public UserQueryHandler(IUserRepository UserRepository, IMapper mapper)
    {
        _mapper = mapper;
        _UserRepository = UserRepository;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        List<User> Users = await _UserRepository.WhereAsync(x => x.IsActive);

        List<UserResponse> UsersResponse = _mapper.Map<List<UserResponse>>(Users);

        return new ApiResponse<List<UserResponse>>(UsersResponse);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User User = await _UserRepository.FirstOrDefaultAsync(x => x.Id == request.id && x.IsActive);
        if (User == null)
        {
            return new ApiResponse<UserResponse>("User not found");
        }

        UserResponse UserResponse = _mapper.Map<UserResponse>(User);

        return new ApiResponse<UserResponse>(UserResponse);
    }
}
