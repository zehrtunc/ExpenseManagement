using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllCustomerPhonesQuery : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(long id) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record CreateCustomerPhoneCommand(CustomerPhoneRequest CustomerPhone) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long id, CustomerPhoneRequest CustomerPhone) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record DeleteCustomerPhoneCommand(long id) : IRequest<ApiResponse>;


