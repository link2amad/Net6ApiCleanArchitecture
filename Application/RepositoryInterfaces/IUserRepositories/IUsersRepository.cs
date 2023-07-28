#region Imports

using Application.Dto;
using Domain.Entities;
using System.Threading.Tasks;

#endregion

namespace Application.RepositoryInterfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> Authenticate(string email, string password);

        Task<User> GetUserByEmail(string email);

        RecordSet<GetUserDto> GetUsers(UserSearchDto userSearchDto);

        User GetUserByID(int UserID);
    }
}