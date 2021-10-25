using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasDefaultValueSql("newId()");

            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(x => x.QuestionId).IsRequired();
            builder.Property(x => x.AnswerText).IsRequired().HasMaxLength(500);

            builder.ToTable("Answers");
        }
    }
}
