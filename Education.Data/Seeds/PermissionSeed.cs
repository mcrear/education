using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Data.Seeds
{
    public class PermissionSeed : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder) => builder.HasData(new Permission
        {
            CreateDate = DateTime.Now,
            CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
            Id = new Guid("2329cc35-3179-4573-ac25-7ff5de3143b9"),
            IsActive = true,
            IsDeleted = false,
            PermissionName = "AddUser"
        });
    }
}
