using Domain.Entities;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
{
    public interface ISystemSettingRepository : IGenericRepository<SystemSetting>
    {
        List<SystemSetting> GetSystemSettings();
    }
}