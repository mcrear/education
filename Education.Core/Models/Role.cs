using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Education.Core.Models
{
    public class Role : _BaseEntity
    {
        public string RoleName { get; set; }
        public virtual ICollection<MapUserRole> Users { get; set; }
        public virtual ICollection<MapRolePermission> Permissions { get; set; }
        public Role()
        {
            Users = new HashSet<MapUserRole>();
            Permissions = new HashSet<MapRolePermission>();
        }
    }
}
