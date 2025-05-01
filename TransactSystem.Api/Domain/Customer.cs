using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transact.Base.Domain;

namespace Transact.Api.Domain;

public class Customer : BaseEntity
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public int CustomerNumber { get; set; }
    public DateTime OpenDate { get; set; }

    public virtual List<CustomerAddress> CustomerAddresses { get; set; }
    public virtual List<CustomerPhone> CustomerPhones { get; set; }
    public virtual List<Account> Accounts { get; set; }
}

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).UseIdentityColumn(); //Id değerinin benzersiz ve artan olabilmesi için 

        builder.Property(b => b.InsertDate).IsRequired(true);
        builder.Property(b => b.UpdateDate).IsRequired(false);
        builder.Property(b => b.InsertUser).IsRequired(true).HasMaxLength(250);
        builder.Property(b => b.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(80);
        builder.HasIndex(c => c.Email).IsUnique(); //Email`in benzersiz olmasi ve sorgunun hızlı yapılabilmesi için

        builder.Property(c => c.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.MiddleName).IsRequired(false).HasMaxLength(50);
        builder.Property(c => c.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.IdentityNumber).IsRequired(true).HasMaxLength(11);
        builder.Property(c => c.CustomerNumber).IsRequired(true);
        builder.Property(c => c.OpenDate).IsRequired(true);

        builder.HasMany(x => x.CustomerAddresses)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CustomerPhones)
           .WithOne(x => x.Customer)
           .HasForeignKey(x => x.CustomerId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Accounts)
           .WithOne(x => x.Customer)
           .HasForeignKey(x => x.CustomerId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CustomerNumber).IsUnique(true);
    }
}
