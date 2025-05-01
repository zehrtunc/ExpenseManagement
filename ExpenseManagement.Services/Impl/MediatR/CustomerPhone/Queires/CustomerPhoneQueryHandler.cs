using MediatR;
using Transact.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Data;
using Transact.Base;
using Transact.Schema;
using AutoMapper;
namespace Transact.Api.Impl.MediatR;

public class CustomerPhoneQueryHandler :
    IRequestHandler<GetAllCustomerPhonesQuery, ApiResponse<List<CustomerPhoneResponse>>>,
    IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerPhoneQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhonesQuery request, CancellationToken cancellationToken)
    {
        List<CustomerPhone> CustomerPhones = await _context.CustomerPhones.Where(x => x.IsActive).ToListAsync(cancellationToken);

        List<CustomerPhoneResponse> CustomerPhonesResponse = _mapper.Map<List<CustomerPhoneResponse>>(CustomerPhones);

        return new ApiResponse<List<CustomerPhoneResponse>>(CustomerPhonesResponse);
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
    {
        CustomerPhone CustomerPhone = await _context.CustomerPhones.FirstAsync(x => x.Id == request.id && x.IsActive);
        if (CustomerPhone == null)
        {
            return new ApiResponse<CustomerPhoneResponse>("CustomerPhone not found");
        }

        CustomerPhoneResponse CustomerPhoneResponse = _mapper.Map<CustomerPhoneResponse>(CustomerPhone);

        return new ApiResponse<CustomerPhoneResponse>(CustomerPhoneResponse);
    }
}
