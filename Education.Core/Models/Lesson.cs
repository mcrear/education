using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Lesson : _BaseEntity
    {
        public string LessonName { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<MapClassroomLesson> Classrooms { get; set; }
        public Lesson()
        {
            Topics = new HashSet<Topic>();
            Classrooms = new HashSet<MapClassroomLesson>();
        }
    }
}
