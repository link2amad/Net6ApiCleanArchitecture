using Application.Dto;
using Application.Helper;
using Application.RepositoryInterfaces;

//using static Application.Services.EmailService;
using Application.ServiceInterfaces.IUserServices;
using AutoMapper;
using Domain.Constants;
using System;
using System.Threading.Tasks;

namespace Application.Services.UserServices
{
    public class AccountService : IAccountService
    {
        private readonly IUsersService _usersService;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly ICurrentUserService _currentUserService;

        public AccountService(IUsersService usersService,
            IUsersRepository usersRepository,
            IMapper mapper,
            ISystemSettingRepository systemSettingRepository,
            ICurrentUserService currentUserService)
        {
            _usersService = usersService;
            _usersRepository = usersRepository;
            _mapper = mapper;
            _systemSettingRepository = systemSettingRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UserDto> AuthenticateUserAccount(string email, string password)
        {
            return await _usersService.Authenticate(email, password);
        }

        public StatusDto ChangePassword(ChangePasswordDto changePasswordVm)
        {
            StatusDto statusVm = new StatusDto();
            var userInDb = _usersRepository.GetFirst(x => x.ID == _currentUserService.UserId && !x.IsDeleted);
            if (userInDb != null)
            {
                if (userInDb.Password.VerifyWithBCrypt(changePasswordVm.CurrentPassword))
                {
                    userInDb.Password = changePasswordVm.NewPassword.WithBCrypt();
                    statusVm.Object = userInDb;
                    _usersRepository.Complete();
                    statusVm.Status = true;
                    statusVm.Message = "Password changed successfully";
                }
                else
                {
                    statusVm.Message = "Incorrect CurrentPassword";
                }
            }
            else
            {
                statusVm.Message = "Password can not be changed at the moment, please try again later";
            }

            return statusVm;
        }

        public StatusDto ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            StatusDto statusVm = new StatusDto();
            var urlExpirySetting = _systemSettingRepository.GetFirst(c => c.SettingName == SystemSettingsVariables.ExpiryTime);
            bool validationStatus = false;
            var userDetail = _usersRepository.GetFirst(x => x.PasswordRequestHash == resetPasswordDto.ResetToken);
            if (userDetail != null)
            {
                validationStatus = DateTime.UtcNow < userDetail.PasswordRequestDate.Value.AddMinutes(Convert.ToInt32(urlExpirySetting?.SettingValue ?? "0"))
                    ? true
                    : false;
                if (validationStatus)
                {
                    userDetail.Password = resetPasswordDto.Password.WithBCrypt();
                    userDetail.ModifiedDate = DateTime.UtcNow;
                    userDetail.PasswordRequestHash = null;
                    _usersRepository.Complete();
                    statusVm.Status = true;
                    statusVm.Message = "Password changed successfully";
                }
                else
                {
                    statusVm.Message = "Token Expired";
                }
            }
            else
            {
                statusVm.Message = "Invalid Token";
            }
            return statusVm;
        }

        public bool UpdateChangePasswordRequestHash(string userName, string changePasswordToken)
        {
            var userObjectToUpdate = _usersRepository.GetUserByEmail(userName).Result;
            if (userObjectToUpdate != null)
            {
                userObjectToUpdate.PasswordRequestHash = changePasswordToken;
                userObjectToUpdate.PasswordRequestDate = DateTime.UtcNow;
                _usersRepository.Complete();
                return true;
            }

            return false;
        }

        public string ValidateChangePasswordUrl(string changePasswordToken, int urlExpiryTime)
        {
            string validationStatus = "Expired";
            var userDetail = _usersRepository.GetFirst(x => x.PasswordRequestHash == changePasswordToken && !x.IsDeleted);
            if (userDetail != null)
                validationStatus = DateTime.UtcNow < userDetail.PasswordRequestDate.Value.AddMinutes(urlExpiryTime)
                    ? userDetail.Email
                    : "Expired";
            return validationStatus;
        }
    }
}