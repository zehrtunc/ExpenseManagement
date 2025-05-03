using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class RoleFluent : BaseFluent<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles);
    }
}
