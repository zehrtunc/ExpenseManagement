using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class ExpenseCategoryFluent : BaseFluent<ExpenseCategory>
{
    public override void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Name).IsRequired(true);
        builder.Property(a => a.Description).IsRequired().HasMaxLength(200);

        builder.HasMany(x => x.Expenses)
            .WithOne(x => x.ExpenseCategory)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(true);
    }
}
