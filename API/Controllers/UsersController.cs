#region Imports

using API.Helpers;
using Application.Dto;
using Application.ExternalDependencies;
using Application.ServiceInterfaces.IUserServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

#endregion

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly IAccountService _accountService;
        private readonly IEmailHandler _emailService;

        public UsersController(
            IUsersService usersService,
            IAccountService accountService,
            IEmailHandler emailService
            )
        {
            _usersService = usersService;
            _accountService = accountService;
            _emailService = emailService;
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserByID")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            return CreateHttpResponse(() =>
            {
                var response = _usersService.GetUserByID(id);
                var isSuccess = response != null;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = isSuccess ? "User got successfully" : "No User found at given id",
                    Result = isSuccess ? response : null,
                    StatusCode = HttpStatusCode.OK,
                    Success = isSuccess
                });
            });
        }

        /// <summary>
        ///     Upsert a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("UpsertUser")]
        public async Task<IActionResult> UpsertUser(UserUpsertDto user)
        {
            return CreateHttpResponse(() =>
            {
                var response = _usersService.Upsert(user).Result;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "User not found" : "User Upserted successfully",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        /// <summary>
        ///     Update a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPatch("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] JsonPatchDocument userDocument)
        {
            return CreateHttpResponse(() =>
            {
                var response = _usersService.UpdateUserPatchAsync(id, userDocument).Result;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "User not found" : "User updated successfully",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        // DELETE api/<Users>/5
        /// <summary>
        ///     Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return CreateHttpResponse(() =>
            {
                var response = _usersService.DeleteUser(id);
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "" : "User removed successfully",
                    //Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        /// <summary>
        ///     Get Users
        /// </summary>
        [HttpGet("GetUsers")]
        public IActionResult GetUsers([FromQuery] UserSearchDto userSearchDto)
        {
            return CreateHttpResponse(() =>
            {
                var response = _usersService.GetUsers(userSearchDto);
                var paginationSet = new PaginationSet<GetUserDto>
                {
                    Items = response.Items,
                    PageIndex = userSearchDto.PageIndex,
                    PageSize = userSearchDto.PageSize,
                    TotalRows = response.TotalRows
                };
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response.Items?.Count == 0 ? "Record Not Found" : "",
                    Result = paginationSet,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }
    }
}