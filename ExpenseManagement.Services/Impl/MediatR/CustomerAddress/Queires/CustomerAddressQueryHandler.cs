using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class CustomerAddressQueryHandler :
    IRequestHandler<GetAllCustomerAddresssQuery, ApiResponse<List<CustomerAddressResponse>>>,
    IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerAddressQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddresssQuery request, CancellationToken cancellationToken)
    {
        List<CustomerAddress> CustomerAddresss = await _context.CustomerAddresses.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<CustomerAddressResponse> CustomerAddresssResponse = _mapper.Map<List<CustomerAddressResponse>>(CustomerAddresss);

        return new ApiResponse<List<CustomerAddressResponse>>(CustomerAddresssResponse);
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
    {
        CustomerAddress CustomerAddress = await _context.CustomerAddresses.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (CustomerAddress == null)
        {
            return new ApiResponse<CustomerAddressResponse>("CustomerAddress not found");
        }

        CustomerAddressResponse CustomerAddressResponse = _mapper.Map<CustomerAddressResponse>(CustomerAddress);

        return new ApiResponse<CustomerAddressResponse>(CustomerAddressResponse);
    }
}
