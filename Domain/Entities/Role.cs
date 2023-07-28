#nullable disable

namespace Domain.Entities
{
    public class Role
    {
        public Role()
        {
            UserToRoles = new HashSet<UserToRole>();
        }

        public int ID { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<UserToRole> UserToRoles { get; set; }
    }
}