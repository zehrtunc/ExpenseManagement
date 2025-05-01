using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transact.Base.Domain;

namespace Transact.Api.Domain;

public class MoneyTransfer : BaseEntity
{
    public long FromAccountId { get; set; }
    public virtual Account FromAccount { get; set; }
    public long ToAccountId { get; set; }
    public virtual Account ToAccount { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal? FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
}

public class MoneyTransferConfiguration : IEntityTypeConfiguration<MoneyTransfer>
{
    public void Configure(EntityTypeBuilder<MoneyTransfer> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).UseIdentityColumn();

        builder.Property(b => b.InsertDate).IsRequired(true);
        builder.Property(b => b.UpdateDate).IsRequired(false);
        builder.Property(b => b.InsertUser).IsRequired(true).HasMaxLength(250);
        builder.Property(b => b.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(m => m.FromAccountId).IsRequired(true);
        builder.Property(m => m.ToAccountId).IsRequired(true);
        builder.Property(m => m.Amount).IsRequired(true).HasPrecision(16, 4);
        builder.Property(m => m.FeeAmount).IsRequired(false).HasPrecision(16, 4);
        builder.Property(m => m.TransactionDate).IsRequired(true);
        builder.Property(m => m.Description).IsRequired(true).HasMaxLength(500);
        builder.Property(m => m.ReferenceNumber).IsRequired(true).HasMaxLength(50);

        builder.HasOne(x => x.FromAccount)
            .WithMany(a => a.MoneyTransfersFrom)
            .HasForeignKey(x => x.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToAccount)
            .WithMany(a => a.MoneyTransfersTo)
            .HasForeignKey(x => x.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}