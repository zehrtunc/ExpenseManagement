using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transact.Base.Domain;

namespace Transact.Api.Domain;

public class CustomerAddress : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}

public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.HasKey(ca => ca.Id);
        builder.Property(ca => ca.Id).UseIdentityColumn();

        builder.Property(b => b.InsertDate).IsRequired(true);
        builder.Property(b => b.UpdateDate).IsRequired(false);
        builder.Property(b => b.InsertUser).IsRequired(true).HasMaxLength(250);
        builder.Property(b => b.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(ca => ca.CountryCode).IsRequired().HasMaxLength(3);
        builder.Property(ca => ca.City).IsRequired().HasMaxLength(100);
        builder.Property(ca => ca.District).IsRequired().HasMaxLength(100);
        builder.Property(ca => ca.Street).IsRequired().HasMaxLength(100);
        builder.Property(ca => ca.ZipCode).IsRequired().HasMaxLength(10);
        builder.Property(ca => ca.IsDefault).IsRequired();
    }
}