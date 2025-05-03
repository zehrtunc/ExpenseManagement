using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class UserFluent : BaseFluent<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Surname).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
        builder.Property(a => a.PasswordHash).IsRequired();

        builder.HasOne(x => x.BankAccount)
            .WithOne(x => x.User)
            .HasForeignKey<BankAccount>(x => x.UserId)
            .IsRequired(true);

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users);

        builder.HasMany(x => x.ExpenseRequests)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired(true);

        builder.HasMany(x => x.ExpenseReviews)
            .WithOne(x => x.ReviewByUser)
            .HasForeignKey(x => x.ReviewById)
            .IsRequired(false);
    }
}
