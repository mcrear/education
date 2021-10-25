using Education.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
