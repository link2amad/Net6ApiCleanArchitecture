using Application.Dto;
using Application.ServiceInterfaces.IUserServices;
using Newtonsoft.Json;
using System.Security.Claims;

namespace API.services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor contextAccessor;
        private UserDto user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            contextAccessor = httpContextAccessor;
            var userInfo = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.UserData);
            if (userInfo != null) user = JsonConvert.DeserializeObject<UserDto>(userInfo);
        }

        public string Fullname
        {
            get
            {
                var fullName = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
                if (String.IsNullOrEmpty(fullName))
                {
                    contextAccessor.HttpContext.Items.TryGetValue("Name", out object contextUserName);
                    if (contextUserName != null)
                    {
                        fullName = contextUserName.ToString();
                    }
                }
                return fullName;
            }
        }

        public UserDto User
        {
            get
            {
                if (user != null) return user;
                //if (!contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                //    throw new UnauthorizedAccessException();
                var userInfo = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.UserData);
                if (userInfo != null) user = JsonConvert.DeserializeObject<UserDto>(userInfo);
                return user ?? new UserDto();
            }
        }

        public int UserId => User.ID;
    }
}