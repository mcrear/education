using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Models
{
    public class MapRolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
