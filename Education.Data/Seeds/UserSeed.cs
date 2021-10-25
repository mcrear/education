using Education.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public UserSeed()
        {
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                    CreateDate = DateTime.Now,
                    CreatedBy = new Guid("749fd010-adcf-476d-832f-8a86e38aeb7d"),
                    Email = "mcrear.master@gmail.com",
                    FirstName = "Ahmet",
                    LastName = "Aydeniz",
                    IsActive = true,
                    IsDeleted = false,
                    Phone = "0 533 160 84 62"
                });
        }
    }
}
