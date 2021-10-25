using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Seeds
{
    public class TopicSeed : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasData(new Topic
            {
                CreateDate = DateTime.Now,
                CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                IsActive = true,
                IsDeleted = false,
                TopicName = "Seed Topic Name",
                LessonId = new Guid("801eb3bf-2f69-40e1-9064-2d854d0dc2e2"),
                Id = new Guid("5e199435-e1ec-47df-a1bf-57f002b1cf01")
            });
        }
    }
}
