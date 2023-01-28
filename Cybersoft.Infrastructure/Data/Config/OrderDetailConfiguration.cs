using Cybersoft.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Infrastructure.Data.Config
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.Property(od => od.OrderId).HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(od => od.ProductId).HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(od => od.Quantity).ValueGeneratedNever().HasMaxLength(6).IsUnicode(false).IsRequired();
            builder.Property(od => od.Price).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(od => od.TotalPrice).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();

            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(od => od.Product).WithMany(p => p.ProductOrderDetails).HasForeignKey(od => od.ProductId).OnDelete(DeleteBehavior.Restrict).IsRequired();

        }
    }
}
