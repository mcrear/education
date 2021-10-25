using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Education.Core.DTOs
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }
        public List<Guid> Permissions { get; set; }
        public RoleDto()
        {
            Permissions = new List<Guid>();
        }
    }
}
