using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Seeds
{
    class AnswerSeed : IEntityTypeConfiguration<Answer>
    {
        private readonly Guid[] _Ids;
        public AnswerSeed(Guid[] Ids)
        {
            _Ids = Ids;
        }

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasData(
                new Answer { }
                );
        }
    }
}
