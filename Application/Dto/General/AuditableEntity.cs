#region Imports

using System;

#endregion

namespace Application.Dto
{
    public abstract class AuditableEntity
    {
        public int ID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}