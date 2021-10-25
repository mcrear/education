using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Models
{
    public class _BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public _BaseEntity()
        {
            
        }
    }
}
