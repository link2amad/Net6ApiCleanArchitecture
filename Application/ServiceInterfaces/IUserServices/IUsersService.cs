#region Imports

using Application.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

#endregion

namespace Application.ServiceInterfaces.IUserServices
{
    public interface IUsersService : IGenericService<User>
    {
        UserDto GetUserByID(int id);

        Task<UserDto> Upsert(UserUpsertDto userUpserDto);

        UserDto DeleteUser(int id);

        Task<UserDto> Authenticate(string email, string password);

        Task<UserDto> UpdateUserPatchAsync(int id, JsonPatchDocument userDocument);

        UserDto GetByEmail(string email);

        RecordSet<GetUserDto> GetUsers(UserSearchDto userSearchDto);
    }
}