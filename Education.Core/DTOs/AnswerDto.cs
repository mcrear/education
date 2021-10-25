using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Education.Core.DTOs
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="{0} alanı gereklidir.")]
        public string AnswerText { get; set; }
        public Guid QuestionId { get; set; }
        public bool? IsActive { get; set; }
    }
}
