using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Permission : _BaseEntity
    {
        public string PermissionName { get; set; }
        public virtual ICollection<MapRolePermission> Roles { get; set; }
        public Permission()
        {
            Roles = new HashSet<MapRolePermission>();
        }
    }
}
