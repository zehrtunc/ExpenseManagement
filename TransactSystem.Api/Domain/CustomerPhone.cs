using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transact.Base.Domain;

namespace Transact.Api.Domain;

public class CustomerPhone : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}

public class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
{
    public void Configure(EntityTypeBuilder<CustomerPhone> builder)
    {
        builder.HasKey(cp => cp.Id);
        builder.Property(cp => cp.Id).UseIdentityColumn();

        builder.Property(b => b.InsertDate).IsRequired(true);
        builder.Property(b => b.UpdateDate).IsRequired(false);
        builder.Property(b => b.InsertUser).IsRequired(true).HasMaxLength(250);
        builder.Property(b => b.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(cp => cp.CountryCode).IsRequired().HasMaxLength(3);
        builder.Property(cp => cp.PhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(cp => cp.IsDefault).IsRequired();
    }
}