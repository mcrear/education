using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Seeds
{
    public class LessonSeed : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasData(
                new Lesson
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                    IsActive = true,
                    IsDeleted = false,
                    LessonName = "Seed Lesson Name",
                    Id = new Guid("801eb3bf-2f69-40e1-9064-2d854d0dc2e2")
                });
        }
    }
}
