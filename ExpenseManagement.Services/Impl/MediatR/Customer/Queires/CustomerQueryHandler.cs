using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class CustomerQueryHandler :
    IRequestHandler<GetAllCustomersQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        List<Customer> customers = await _context.Customers.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<CustomerResponse> customersResponse = _mapper.Map<List<CustomerResponse>>(customers);

        return new ApiResponse<List<CustomerResponse>>(customersResponse);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        Customer customer = await _context.Customers.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (customer == null)
        {
            return new ApiResponse<CustomerResponse>("Customer not found");
        }

        CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(customer);

        return new ApiResponse<CustomerResponse>(customerResponse);
    }
}
