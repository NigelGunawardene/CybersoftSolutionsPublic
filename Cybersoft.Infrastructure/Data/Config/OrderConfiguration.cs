using Cybersoft.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            //builder.Ignore(e => e.OrderProducts);
            builder.Property(o => o.PublicOrderNumber).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(o => o.Message).ValueGeneratedNever().HasMaxLength(500).IsUnicode(false).IsRequired(false);
            builder.Property(o => o.UserId).HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(o => o.TotalPrice).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(o => o.OrderDate).HasDefaultValueSql("getutcdate()").HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(o => o.IsComplete).HasMaxLength(1).IsUnicode(false).IsRequired();
            builder.Property(o => o.IsDeleted).HasMaxLength(1).IsUnicode(false).IsRequired();

            builder.HasOne(o => o.User).WithMany(u => u.OrderList).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
