using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Education.Core.DTOs
{
    public class TopicDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string TopicName { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public Guid LessonId { get; set; }
        public bool? IsActive { get; set; }
    }
}
