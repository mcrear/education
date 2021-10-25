using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.DTOs
{
    public class RoleListDto
    {
        public List<RoleDto> Roles { get; set; }
        public RoleListDto()
        {
            Roles = new List<RoleDto>();
        }
    }
}
