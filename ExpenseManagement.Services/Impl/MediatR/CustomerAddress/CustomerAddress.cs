using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllCustomerAddresssQuery : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(long id) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record CreateCustomerAddressCommand(CustomerAddressRequest CustomerAddress) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long id, CustomerAddressRequest CustomerAddress) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record DeleteCustomerAddressCommand(long id) : IRequest<ApiResponse>;


