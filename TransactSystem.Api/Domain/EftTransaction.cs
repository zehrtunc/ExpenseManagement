using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transact.Base.Domain;

namespace Transact.Api.Domain;

public class EftTransaction : BaseEntity
{
    public long FromAccountId { get; set; }
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal? FeeAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string? PaymentCategoryCode { get; set; }
}

public class EftTransactionConfiguration : IEntityTypeConfiguration<EftTransaction>
{
    public void Configure(EntityTypeBuilder<EftTransaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.FromAccountId).IsRequired(true);
        builder.HasIndex(x => x.FromAccountId).IsUnique(false);

        builder.Property(x => x.ReceiverName).IsRequired(true).HasMaxLength(500);
        builder.Property(x => x.ReceiverIban).IsRequired(true).HasMaxLength(26);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(16, 4);
        builder.Property(x => x.FeeAmount).IsRequired(false).HasPrecision(16, 4);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.PaymentCategoryCode).IsRequired(false).HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(500);
        builder.Property(x => x.ReferenceNumber).IsRequired(true).HasMaxLength(50);
        
    }
}