using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class BankAccountFluent : BaseFluent<BankAccount>
{
    public override void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.AccountNumber).IsRequired();
        builder.Property(a => a.IBAN).IsRequired().HasMaxLength(26);
        builder.Property(a => a.CurrencyCode).IsRequired().HasMaxLength(3);

        builder.HasOne(x => x.User)
            .WithOne(x => x.BankAccount)
            .HasForeignKey<BankAccount>(x => x.UserId)
            .IsRequired(true);
    }
}
