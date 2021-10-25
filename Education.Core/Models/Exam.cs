using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Exam : _BaseEntity
    {
        public string ExamName { get; set; }
        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<MapExamQuestion> Questions { get; set; }
        public Exam()
        {
            Questions = new HashSet<MapExamQuestion>();
        }
    }
}
