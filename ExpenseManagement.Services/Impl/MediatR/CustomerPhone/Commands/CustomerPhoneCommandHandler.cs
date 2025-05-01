using AutoMapper;
using MediatR;
using Transact.Api.Data;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public class CustomerPhoneCommandHandler :
     IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
     IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
     IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerPhoneCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        CustomerPhoneRequest CustomerPhoneRequest = request.CustomerPhone;
        CustomerPhone CustomerPhone = _mapper.Map<CustomerPhone>(CustomerPhoneRequest);
        CustomerPhone.InsertDate = DateTime.Now;
        CustomerPhone.InsertUser = "admin";
        CustomerPhone.IsActive = true;

        await _context.CustomerPhones.AddAsync(CustomerPhone, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        CustomerPhoneResponse CustomerPhoneResponse = _mapper.Map<CustomerPhoneResponse>(CustomerPhone);

        return new ApiResponse<CustomerPhoneResponse>(CustomerPhoneResponse);
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        CustomerPhone CustomerPhoneEntity = await _context.CustomerPhones.FindAsync(request.id, cancellationToken);

 
        if (CustomerPhoneEntity == null)
        {
            return new ApiResponse<CustomerPhoneResponse>("CustomerPhone not found");
        }

        CustomerPhoneEntity.CustomerId = request.CustomerPhone.CustomerId;
        CustomerPhoneEntity.CountryCode = request.CustomerPhone.CountryCode;
        CustomerPhoneEntity.PhoneNumber = request.CustomerPhone.PhoneNumber;
        CustomerPhoneEntity.IsDefault = request.CustomerPhone.IsDefault;
        CustomerPhoneEntity.UpdateUser = "admin";
        CustomerPhoneEntity.UpdateDate = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);


        CustomerPhoneResponse CustomerPhoneResponse = _mapper.Map<CustomerPhoneResponse>(CustomerPhoneEntity);

        return new ApiResponse<CustomerPhoneResponse>(CustomerPhoneResponse);
    }

    public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        CustomerPhone CustomerPhone = await _context.CustomerPhones.FindAsync(request.id, cancellationToken);

        if (CustomerPhone == null) return new ApiResponse("CustomerPhone not found");

        CustomerPhone.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
