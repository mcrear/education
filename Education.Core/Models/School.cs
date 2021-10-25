using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class School : _BaseEntity
    {
        public string SchoolName { get; set; }
        public virtual ICollection<Classroom> Classrooms { get; set; }
        public virtual ICollection<MapUserSchool> Users { get; set; }
        public School()
        {
            Classrooms = new HashSet<Classroom>();
            Users = new HashSet<MapUserSchool>();
        }
    }
}
