using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Models
{
    public class QuestionType : _BaseEntity
    {
        public string TypeName { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }
    }
}
