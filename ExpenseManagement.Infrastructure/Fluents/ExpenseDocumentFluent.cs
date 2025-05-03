using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Infrastructure.Fluents;

public class ExpenseDocumentFluent : BaseFluent<ExpenseDocument>
{
    public override void Configure(EntityTypeBuilder<ExpenseDocument> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.FilePath).IsRequired().HasMaxLength(500);
        builder.Property(a => a.FileType).IsRequired().HasMaxLength(10);

        builder.HasOne(x => x.Expense)
            .WithMany(x => x.ExpenseDocuments)
            .HasForeignKey(x => x.ExpenseId)
            .IsRequired(true);
    }
}
