using Application.Dto;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces.IGeneralServices;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services.GeneralServices
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly IMapper _mapper;
        private readonly ISystemSettingRepository _systemSettingRepository;

        public SystemSettingService(ISystemSettingRepository systemSettingRepository, IMapper mapper)
        {
            _systemSettingRepository = systemSettingRepository;
            _mapper = mapper;
        }

        public List<SystemSettingDto> GetSystemSettings()
        {
            var systemSettings = _systemSettingRepository.GetSystemSettings();
            return _mapper.Map<List<SystemSetting>, List<SystemSettingDto>>(systemSettings);
        }
    }
}