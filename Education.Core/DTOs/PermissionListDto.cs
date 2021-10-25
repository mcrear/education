using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.DTOs
{
    public class PermissionListDto
    {
        public List<PermissionDto> Permssions { get; set; }
        public PermissionListDto()
        {
            Permssions = new List<PermissionDto>();
        }
    }
}
