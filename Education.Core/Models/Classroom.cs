using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Classroom : _BaseEntity
    {
        public string ClassName { get; set; }
        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<MapClassroomLesson> Lessons { get; set; }
        public virtual ICollection<MapUserClassroom> Users { get; set; }
        public Classroom()
        {
            Lessons = new HashSet<MapClassroomLesson>();
            Users = new HashSet<MapUserClassroom>();
        }
    }
}
