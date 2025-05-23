﻿using ExpenseManagement.Base.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Infrastructure.Fluents;

public class BaseFluent<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).UseIdentityColumn();

        builder.Property(b => b.InsertDate).IsRequired(false);
        builder.Property(b => b.UpdateDate).IsRequired(false);
        builder.Property(b => b.InsertUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.UpdateUser).IsRequired(false).HasMaxLength(250);
        builder.Property(b => b.IsActive).IsRequired(true).HasDefaultValue(true);
    }
}