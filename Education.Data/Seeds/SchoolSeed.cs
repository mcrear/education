using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Data.Seeds
{
    public class SchoolSeed : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasData(
                new School
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                    Id = new Guid("ad6c1b19-039b-41d5-a44a-de2babd074f5"),
                    IsActive = true,
                    IsDeleted = false,
                    Classrooms = new Collection<Classroom>(),
                    SchoolName = "Seed School Name"
                });
        }
    }
}
