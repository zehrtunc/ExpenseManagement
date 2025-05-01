using AutoMapper;
using Transact.Api.Domain;
using Transact.Api.Impl.MediatR;
using Transact.Schema;

namespace Transact.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();

            CreateMap<AccountRequest, Account>();
            CreateMap<Account, AccountResponse>();

            CreateMap<AccountTransactionRequest, AccountTransaction>();
            CreateMap<AccountTransaction, AccountTransactionResponse>();

            CreateMap<CustomerAddressRequest, CustomerAddress>();
            CreateMap<CustomerAddress, CustomerAddressResponse>();

            CreateMap<CustomerPhoneRequest, CustomerPhone>();
            CreateMap<CustomerPhone, CustomerPhoneResponse>();

            CreateMap<EftTransactionRequest, EftTransaction>();
            CreateMap<EftTransaction, EftTransactionResponse>();

            CreateMap<MoneyTransferRequest, MoneyTransfer>();
            CreateMap<MoneyTransfer, MoneyTransferResponse>();
        }
    }
}
