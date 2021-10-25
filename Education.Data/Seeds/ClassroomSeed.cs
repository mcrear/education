using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Data.Seeds
{
    public class ClassroomSeed : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData(
                new Classroom
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                    SchoolId = new Guid("ad6c1b19-039b-41d5-a44a-de2babd074f5"),
                    IsActive = true,
                    IsDeleted = false,
                    ClassName = "Seed Class Name",
                    Id = new Guid("7a2dd1e4-d9d1-460c-bfeb-d0fc93989e24")
                });
        }
    }
}
