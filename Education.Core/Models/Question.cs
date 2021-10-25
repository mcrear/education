using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Question : _BaseEntity
    {
        public string QuestionText { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public Guid RightAnswer { get; set; }
        public string TagText { get; set; }
        public Guid QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
        public virtual ICollection<MapExamQuestion> Exams { get; set; }
        public Question()
        {
            Answers = new HashSet<Answer>();
            Exams = new HashSet<MapExamQuestion>();
        }
    }
}
