using API.Helpers;
using Application.Configurations;
using Application.Dto;
using Application.ServiceInterfaces.IGeneralServices;
using Application.ServiceInterfaces.IUserServices;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    /// <summary>
    /// Authentication Related Endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUsersService _usersService;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISystemSettingService _systemSettingService;
        private readonly AppSettings _appSettings;

        public AuthController(IAccountService accountService, IEmailService emailService,
                              IUsersService usersService, ICurrentUserService currentUserService,
                              IOptions<AppSettings> appSettings, ISystemSettingService systemSettingService)
        {
            _accountService = accountService;
            _usersService = usersService;
            _currentUserService = currentUserService;
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _systemSettingService = systemSettingService;
        }

        /// <summary>
        /// Authenticates the user against given username/password
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>JWT accessToken along with user data</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Authenticate(LoginDto loginRequest)
        {
            try
            {
                UserDto user = await _accountService.AuthenticateUserAccount(loginRequest.Email, loginRequest.Password);
                UserWithTokenDto userWithTokenVm = new UserWithTokenDto()
                {
                    User = user
                };

                if (user != null && user.ID != 0)
                {
                    // Set time for expire token
                    DateTime tokenExpiryTime = DateTime.UtcNow;
                    tokenExpiryTime = tokenExpiryTime.AddMinutes(_appSettings.JwtTokenExpireInMinute ?? 60); // TODO : move to appsettings
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret); // TODO : move to appsettings

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Actor, user.ID.ToString()),
                        new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
                        }),
                        Expires = tokenExpiryTime,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    foreach (var item in user.Roles)
                    {
                        tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item));
                    }
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    userWithTokenVm.accessToken = tokenHandler.WriteToken(token);
                    userWithTokenVm.RequestedServerUtcNow = DateTime.UtcNow;
                    userWithTokenVm.ExpiresAt = tokenExpiryTime;

                    return new OkObjectResult(new SuccessResponseVM { Message = "The request successful", Result = userWithTokenVm, StatusCode = HttpStatusCode.OK, Success = true });
                }
                return new OkObjectResult(new SuccessResponseVM { Message = "User not found", Result = null, StatusCode = HttpStatusCode.Unauthorized, Success = false });
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new ErrorResponseVM
                {
                    Message = "The request is invalid.",
                    Result = new ErrorDetailVM
                    {
                        ExceptionMessage = ex.Message,
                        ExceptionMessageDetail = ex.InnerException != null ? ex.InnerException.ToString() : string.Empty
                    },
                    StatusCode = HttpStatusCode.ExpectationFailed,
                    Success = false
                });
            }
        }

        /// <summary>
        /// Returns User Data based on JWT Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get()
        {
            try
            {
                return new OkObjectResult(new SuccessResponseVM { Message = "The request successful", Result = _currentUserService.User, StatusCode = HttpStatusCode.OK, Success = true });
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new ErrorResponseVM
                {
                    Message = "The request is invalid.",
                    Result = new ErrorDetailVM
                    {
                        ExceptionMessage = ex.Message,
                        ExceptionMessageDetail = ex.InnerException != null ? ex.InnerException.ToString() : string.Empty
                    },
                    StatusCode = HttpStatusCode.ExpectationFailed,
                    Success = false
                });
            }
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordVM)
        {
            try
            {
                var user = _accountService.ChangePassword(changePasswordVM);
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = user.Message,
                    Result = null,
                    StatusCode = HttpStatusCode.OK,
                    Success = user.Status
                });
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new ErrorResponseVM
                {
                    Message = "The request is invalid.",
                    Result = new ErrorDetailVM
                    {
                        ExceptionMessage = ex.Message,
                        ExceptionMessageDetail = ex.InnerException != null ? ex.InnerException.ToString() : string.Empty
                    },
                    StatusCode = HttpStatusCode.ExpectationFailed,
                    Success = false
                });
            }
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                return CreateHttpResponse(() =>
                {
                    var result = _accountService.ResetPassword(resetPasswordDto);
                    return new OkObjectResult(new SuccessResponseVM
                    {
                        Message = result.Message,
                        Result = null,
                        StatusCode = HttpStatusCode.OK,
                        Success = result.Status
                    });
                });
            }
            catch (Exception exception)
            {
                return CreateHttpResponse(() =>
                {
                    return new OkObjectResult(new SuccessResponseVM
                    {
                        Message = exception.Message,
                        Result = exception,
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Success = false
                    });
                });
            }
        }

        [HttpGet("validateChangePasswordURL")]
        public IActionResult validateChangePasswordURL(string id)
        {
            try
            {
                var systemSetting = _systemSettingService.GetSystemSettings();
                var expiryTime = systemSetting
                                 .Where(x => x.SettingName == SystemSettingsVariables.ExpiryTime).FirstOrDefault()
                                 .SettingValue;
                return CreateHttpResponse(() =>
                {
                    return new OkObjectResult(new SuccessResponseVM
                    {
                        Message = "",
                        Result = _accountService.ValidateChangePasswordUrl(id, Convert.ToInt32(expiryTime)),
                        StatusCode = HttpStatusCode.OK,
                        Success = true
                    });
                });
            }
            catch (Exception exception)
            {
                return CreateHttpResponse(() =>
                {
                    return new OkObjectResult(new SuccessResponseVM
                    {
                        Message = exception.Message,
                        Result = exception,
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Success = false
                    });
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string changePasswordToken = Guid.NewGuid().ToString();
                bool result = _accountService.UpdateChangePasswordRequestHash(email, changePasswordToken);
                string msg = "";
                if (result)
                {
                    result = _emailService.SendResetPasswordEmail(_usersService.GetByEmail(email));
                    if (result)
                    {
                        msg = "Recovery email sent successfully. Please follow the instructions provided in the email to reset your password.";
                    }
                    else
                    {
                        msg = "Something went wrong while sending recovery email";
                    }
                }
                else
                {
                    msg = "User not found";
                }
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = msg,
                    Result = result,
                    StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                    Success = result
                });
            }
            catch (Exception exception)
            {
                return CreateHttpResponse(() =>
                {
                    return new OkObjectResult(new SuccessResponseVM
                    {
                        Message = exception.Message,
                        Result = exception,
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Success = false
                    });
                });
            }
        }

        private int VerifyToken(string token)
        {
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
            var validationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            int id = 0;
            try
            {
                SecurityToken validatedToken = null;
                var claims = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                if (validatedToken != null)
                {
                    id = Convert.ToInt32(claims.FindFirstValue(ClaimTypes.NameIdentifier));
                }
            }
            catch (SecurityTokenException)
            {
                return id;
            }
            return id;
        }
    }
}