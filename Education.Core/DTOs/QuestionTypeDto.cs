using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Education.Core.DTOs
{
    public class QuestionTypeDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string TypeName { get; set; }
        public bool? IsActive { get; set; }
    }
}
