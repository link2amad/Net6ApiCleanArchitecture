#region Imports

using Domain.Entities;
using System.Collections.Generic;

#endregion

namespace Application.RepositoryInterfaces
{
    public interface IStateRepository : IGenericRepository<State>
    {
        State GetState(string name);

        List<State> GetAllStates(bool? active = null);
    }
}