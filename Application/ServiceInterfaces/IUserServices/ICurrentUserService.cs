using Application.Dto;

namespace Application.ServiceInterfaces.IUserServices
{
    public interface ICurrentUserService
    {
        string Fullname { get; }
        public int UserId { get; }
        UserDto User { get; }
    }
}