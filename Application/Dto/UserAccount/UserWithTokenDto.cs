using System;

namespace Application.Dto
{
    public class UserWithTokenDto
    {
        public string accessToken { get; set; }
        public UserDto User { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime RequestedServerUtcNow { get; set; }
    }
}