using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class CustomerCommandHandler :
     IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
     IRequestHandler<UpdateCustomerCommand, ApiResponse<CustomerResponse>>,
     IRequestHandler<DeleteCustomerCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        CustomerRequest customerRequest = request.customer;
        Customer customer = _mapper.Map<Customer>(customerRequest);
        customer.InsertDate = DateTime.Now;
        customer.InsertUser = "admin";
        customer.CustomerNumber = new Random().Next(1000000, 999999999);
        customer.IsActive = true;
        customer.OpenDate = DateTime.Now;

        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(customer);

        return new ApiResponse<CustomerResponse>(customerResponse);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customerEntity = await _context.Customers.FindAsync(request.id, cancellationToken);

        // customer var mı? 
        if (customerEntity == null)
        {
            return new ApiResponse<CustomerResponse>("Customer not found");
        }

        //Entity gelen veri olarak güncellenir
        customerEntity.FirstName = request.customer.FirstName;
        customerEntity.MiddleName = request.customer.MiddleName;
        customerEntity.LastName = request.customer.LastName;
        customerEntity.Email = request.customer.Email;
        customerEntity.UpdateDate = DateTime.Now;
        customerEntity.UpdateUser = "admin";

        await _context.SaveChangesAsync(cancellationToken);


        CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(customerEntity);

        return new ApiResponse<CustomerResponse>(customerResponse);
    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = await _context.Customers.FindAsync(request.id, cancellationToken);

        if (customer == null) return new ApiResponse("Customer not found");

        customer.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
