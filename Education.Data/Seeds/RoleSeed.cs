using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Data.Seeds
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public RoleSeed()
        {

        }

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                    Id = new Guid("3ade6a61-bef9-472e-bb74-ea53e02ef763"),
                    IsActive = true,
                    IsDeleted = false,
                    RoleName = "Admin"
                });
        }
    }
}
