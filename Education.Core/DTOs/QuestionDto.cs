using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Education.Core.DTOs
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string QuestionText { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public Guid RightAnswer { get; set; }
        public string TagText { get; set; }
        public List<AnswerDto> Answers { get; set; }
        public QuestionDto()
        {
            Answers = new List<AnswerDto>();
        }
    }
}
