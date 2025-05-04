using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class PaymentTransactionFluent : BaseFluent<PaymentTransaction>
{
    public override void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Amount).IsRequired().HasPrecision(18, 2);
        builder.Property(a => a.ReferenceNumber).IsRequired().HasMaxLength(50);

        builder.HasOne(x => x.Expense)
            .WithOne(x => x.PaymentTransaction)
            .HasForeignKey<PaymentTransaction>(x => x.ExpenseId)
            .IsRequired(true);

        builder.HasOne(x => x.BankAccount)
            .WithMany(x => x.PaymentTransactions)
            .HasForeignKey(x => x.BankAccountId)
            .IsRequired(true);
    }
}
