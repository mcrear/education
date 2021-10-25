using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Models
{
    public class Answer : _BaseEntity
    {
        public string AnswerText { get; set; }
        public virtual Question Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
