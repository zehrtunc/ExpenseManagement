using AutoMapper;
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Schema;

namespace ExpenseManagement.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BankAccountRequest, BankAccount>();
            CreateMap<BankAccount, BankAccountResponse>();

        }
    }
}
