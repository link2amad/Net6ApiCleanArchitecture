using System;

namespace Application.Dto
{
    public class UserDto : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string PasswordRequestHash { get; set; }
        public DateTime? PasswordRequestDate { get; set; }
        public string[] Roles { get; set; }
    }
}