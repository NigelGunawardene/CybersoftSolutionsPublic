using Cybersoft.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Infrastructure.Data.Config
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.Property(u => u.UserName).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(u => u.FirstName).ValueGeneratedNever().HasMaxLength(50).IsUnicode(false).IsRequired();
            builder.Property(u => u.LastName).ValueGeneratedNever().HasMaxLength(50).IsUnicode(false).IsRequired();
            builder.Property(u => u.FullName).HasComputedColumnSql("[LastName] + ', ' + [FirstName]").HasMaxLength(100).IsUnicode(false).IsRequired();
            builder.Property(u => u.Password).ValueGeneratedNever().HasMaxLength(100).IsUnicode(false).IsRequired();
            builder.Property(u => u.Email).ValueGeneratedNever().HasMaxLength(50).IsUnicode(false).IsRequired();
            builder.Property(u => u.Role).ValueGeneratedNever().HasMaxLength(1).IsUnicode(false).IsRequired();
            builder.Property(u => u.PhoneNumber).ValueGeneratedNever().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(u => u.JoinedDate).HasDefaultValueSql("getutcdate()").HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(u => u.RefreshToken).ValueGeneratedNever().HasMaxLength(100).IsUnicode(false).IsRequired(false);
            builder.Property(u => u.RefreshTokenAddedTime).HasDefaultValueSql("getutcdate()").HasMaxLength(20).IsUnicode(false).IsRequired();
        }
    }
}
