using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class CustomerAddressCommandHandler :
     IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
     IRequestHandler<UpdateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
     IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerAddressCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        CustomerAddressRequest CustomerAddressRequest = request.CustomerAddress;
        CustomerAddress CustomerAddress = _mapper.Map<CustomerAddress>(CustomerAddressRequest);
        CustomerAddress.InsertDate = DateTime.Now;
        CustomerAddress.InsertUser = "admin";
        CustomerAddress.IsActive = true;

        await _context.CustomerAddresses.AddAsync(CustomerAddress, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        CustomerAddressResponse CustomerAddressResponse = _mapper.Map<CustomerAddressResponse>(CustomerAddress);

        return new ApiResponse<CustomerAddressResponse>(CustomerAddressResponse);
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        CustomerAddress CustomerAddressEntity = await _context.CustomerAddresses.FindAsync(request.id, cancellationToken);

        if (CustomerAddressEntity == null)
        {
            return new ApiResponse<CustomerAddressResponse>("CustomerAddress not found");
        }

        CustomerAddressEntity.CustomerId = request.CustomerAddress.CustomerId;
        CustomerAddressEntity.City = request.CustomerAddress.City;
        CustomerAddressEntity.District = request.CustomerAddress.District;
        CustomerAddressEntity.CountryCode = request.CustomerAddress.CountryCode;
        CustomerAddressEntity.Street = request.CustomerAddress.Street;
        CustomerAddressEntity.ZipCode = request.CustomerAddress.ZipCode;
        CustomerAddressEntity.IsDefault = request.CustomerAddress.IsDefault;
        CustomerAddressEntity.UpdateDate = DateTime.Now;
        CustomerAddressEntity.UpdateUser = "admin";

        await _context.SaveChangesAsync(cancellationToken);


        CustomerAddressResponse CustomerAddressResponse = _mapper.Map<CustomerAddressResponse>(CustomerAddressEntity);

        return new ApiResponse<CustomerAddressResponse>(CustomerAddressResponse);
    }

    public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        CustomerAddress CustomerAddress = await _context.CustomerAddresses.FindAsync(request.id, cancellationToken);

        if (CustomerAddress == null) return new ApiResponse("CustomerAddress not found");

        CustomerAddress.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
