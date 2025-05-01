using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transact.Api.Domain;
using Transact.Base.Domain;

namespace Transact.Api.Domain;

public class Account : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string Name { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }

    public virtual List<AccountTransaction> AccountTransactions { get; set; }
    public virtual List<MoneyTransfer> MoneyTransfersFrom { get; set; }
    public virtual List<MoneyTransfer> MoneyTransfersTo { get; set; }

}


public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).UseIdentityColumn();

        builder.Property(b => b.InsertDate).IsRequired(true);
        builder.Property(b => b.UpdateDate).IsRequired(false);
        builder.Property(b => b.InsertUser).IsRequired(true).HasMaxLength(250);
        builder.Property(b => b.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(a => a.CustomerId).IsRequired(true);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.AccountNumber).IsRequired();
        builder.Property(a => a.IBAN).IsRequired().HasMaxLength(26);
        builder.Property(a => a.Balance).IsRequired().HasPrecision(18, 2);
        builder.Property(a => a.CurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(a => a.OpenDate).IsRequired();
        builder.Property(a => a.CloseDate).IsRequired(true);

        builder.HasMany(x => x.AccountTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.AccountNumber).IsUnique(true);
    }
}
