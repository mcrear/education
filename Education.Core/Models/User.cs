using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class User : _BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public virtual ICollection<MapUserRole> Roles { get; set; }
        public virtual ICollection<MapUserClassroom> ClassroomUsers { get; set; }
        public virtual ICollection<MapUserSchool> SchoolUsers { get; set; }
        public User()
        {
            Roles = new HashSet<MapUserRole>();
            ClassroomUsers = new HashSet<MapUserClassroom>();
            SchoolUsers = new HashSet<MapUserSchool>();
        }
    }
}
