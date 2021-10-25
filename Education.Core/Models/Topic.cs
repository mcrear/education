using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Topic : _BaseEntity
    {
        public string TopicName { get; set; }
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public Topic()
        {
            Exams = new HashSet<Exam>();
        }
    }
}
