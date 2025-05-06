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
            CreateMap<Expense, ExpenseResponse>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname))
                 .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ExpenseCategory.Name))
                 .ForMember(dest => dest.ReviewByUserName, opt => opt.MapFrom(src => src.ReviewByUser.Name + " " + src.ReviewByUser.Surname))
                 ;

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
