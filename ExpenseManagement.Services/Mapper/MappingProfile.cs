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

            CreateMap<ExpenseRequest, Expense>();
            CreateMap<Expense, ExpenseResponse>();

            CreateMap<ExpenseCategoryRequest, ExpenseCategory>();
            CreateMap<ExpenseCategory, ExpenseCategoryResponse>();

            CreateMap<ExpenseDocumentRequest, ExpenseDocument>();
            CreateMap<ExpenseDocument, ExpenseDocumentResponse>();

            CreateMap<PaymentTransactionRequest, PaymentTransaction>();
            CreateMap<PaymentTransaction, PaymentTransactionResponse>();

            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();

            CreateMap<RoleRequest, Role>();
            CreateMap<Role, RoleResponse>();
        }
    }
}
