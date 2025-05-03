
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure;

public class PaymentTransactionRepository : BaseRepository<PaymentTransaction>, IPaymentTransactionRepository
{
    public PaymentTransactionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
