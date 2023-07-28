#region Imports

using Domain.Entities;
using System.Collections.Generic;

#endregion

namespace Application.RepositoryInterfaces
{
    public interface IGenderRepository : IGenericRepository<Gender>
    {
        Gender GetGender(string name);

        List<Gender> GetAllGenders(bool? active = null);
    }
}