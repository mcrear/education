using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Seeds
{
    public class ExamSeed : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasData(new Exam
            {
                CreateDate = DateTime.Now,
                CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                IsActive = true,
                IsDeleted = false,
                ExamName = "Seed Exam Name",
                Id = new Guid("95b4b8c1-5447-4e2f-a4af-86393dd82a93"),
                TopicId = new Guid("5e199435-e1ec-47df-a1bf-57f002b1cf01")
            });
        }
    }
}
