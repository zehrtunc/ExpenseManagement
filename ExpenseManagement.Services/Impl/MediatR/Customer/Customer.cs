using MediatR;
using Transact.Api.Domain;
using Transact.Base;
using Transact.Schema;

namespace Transact.Api.Impl.MediatR;

public record GetAllCustomersQuery : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerByIdQuery(long id) : IRequest<ApiResponse<CustomerResponse>>;
public record CreateCustomerCommand(CustomerRequest customer) : IRequest<ApiResponse<CustomerResponse>>;
public record UpdateCustomerCommand(long id, CustomerRequest customer) : IRequest<ApiResponse<CustomerResponse>>;
public record DeleteCustomerCommand(long id) : IRequest<ApiResponse>;


