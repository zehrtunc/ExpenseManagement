using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Infrastructure.Fluents;

public class ExpenseFluent : BaseFluent<Expense>
{
    public override void Configure(EntityTypeBuilder<Expense> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.UserId).IsRequired(true);
        builder.Property(a => a.CategoryId).IsRequired(true);
        builder.Property(a => a.RequestDate).IsRequired(true);
        builder.Property(a => a.Status).HasColumnType("smallint").IsRequired(true);
        builder.Property(a => a.Location).IsRequired(false).HasMaxLength(100);
        builder.Property(a => a.RejectionReason).IsRequired(false).HasMaxLength(200);

        builder.HasOne(x => x.User)
            .WithMany(x => x.ExpenseRequests)
            .HasForeignKey(x => x.UserId)
            .IsRequired(true);

        builder.HasOne(x => x.ReviewByUser)
            .WithMany(x => x.ExpenseReviews)
            .HasForeignKey(x => x.ReviewById)
            .IsRequired(false);


        builder.HasOne(x => x.PaymentTransaction)
            .WithOne(x => x.Expense)
            .HasForeignKey<PaymentTransaction>(x => x.ExpenseId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExpenseCategory)
            .WithMany(x => x.Expenses)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(true);

        builder.HasMany(x => x.ExpenseDocuments)
            .WithOne(x => x.Expense)
            .HasForeignKey(x => x.ExpenseId)
            .IsRequired(true);
    }
}
