using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class AccountFluent : BaseFluent<Account>
{
    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.CustomerId).IsRequired(true);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.AccountNumber).IsRequired();
        builder.Property(a => a.IBAN).IsRequired().HasMaxLength(26);
        builder.Property(a => a.Balance).IsRequired().HasPrecision(18, 2);
        builder.Property(a => a.CurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(a => a.OpenDate).IsRequired();
        builder.Property(a => a.CloseDate).IsRequired(true);

        //builder.HasMany(x => x.AccountTransactions)
        //    .WithOne(x => x.Account)
        //    .HasForeignKey(x => x.AccountId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AccountNumber).IsUnique(true);
    }
}
