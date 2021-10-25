using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Core.Request
{
    public class QuestionRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public string TagText { get; set; }
        public List<AnswerDto> Answers { get; set; }
        public Guid QuestionTypeId { get; set; }
        public QuestionRequest()
        {
            Answers = new List<AnswerDto>();
        }
    }
}
