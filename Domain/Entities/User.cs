#nullable disable

namespace Domain.Entities
{
    public class User : BaseModel
    {
        public User()
        {
            //Files = new HashSet<File>();
            UserToRoles = new HashSet<UserToRole>();
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string PasswordRequestHash { get; set; }
        public DateTime? PasswordRequestDate { get; set; }
        public bool IsDeleted { get; set; }
        //public virtual User CreatedByUser { get; set; }
        //public virtual User ModifiedByUser { get; set; }
        //public virtual ICollection<File> Files { get; set; }

        //public virtual ICollection<User> InverseCreatedByUser { get; set; }
        //public virtual ICollection<User> InverseModifiedByUser { get; set; }
        public virtual ICollection<UserToRole> UserToRoles { get; set; }
    }
}