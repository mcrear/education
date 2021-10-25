using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasDefaultValueSql("newId()");

            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);


            builder.ToTable("Permissions");
        }
    }
}
