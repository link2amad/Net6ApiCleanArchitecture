using Application.Dto;

namespace Application.ServiceInterfaces.IGeneralServices
{
    public interface IEmailService
    {
        bool SendResetPasswordEmail(UserDto userDetail);
    }
}