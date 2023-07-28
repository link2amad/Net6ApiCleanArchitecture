using Application.Dto;
using Application.Dto;
using System.Collections.Generic;

namespace Application.ServiceInterfaces.IGeneralServices
{
    public interface ISystemSettingService
    {
        List<SystemSettingDto> GetSystemSettings();
    }
}