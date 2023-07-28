#nullable disable

namespace Domain.Entities
{
    public class UserToRole
    {
        public int ID { get; set; }
        public int fk_UserID { get; set; }
        public int fk_RoleID { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}