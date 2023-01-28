using Cybersoft.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Infrastructure.Data.Config
{
    public class CartConfiguration : IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> builder)
        {
            builder.Property(ci => ci.UserId).HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(ci => ci.ProductId).HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(ci => ci.ProductName).ValueGeneratedNever().HasMaxLength(100).IsUnicode(false).IsRequired();
            builder.Property(ci => ci.Quantity).ValueGeneratedNever().HasMaxLength(5).IsUnicode(false).IsRequired();
            builder.Property(ci => ci.Price).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(ci => ci.AddedDate).HasDefaultValueSql("getutcdate()").HasMaxLength(20).IsUnicode(false).IsRequired();

            builder.HasOne(ci => ci.User).WithMany(o => o.Cart).HasForeignKey(ci => ci.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(ci => ci.Product).WithMany(p => p.CartItems).HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
