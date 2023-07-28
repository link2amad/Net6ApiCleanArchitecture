#region Imports

using Application.Dto;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserUpsertDto>()
                .ReverseMap();
            CreateMap<UserUpsertDto, User>()
                .ReverseMap();
            CreateMap<SystemSetting, SystemSettingDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.UserToRoles.Select(c => c.Role.RoleName).ToList() ?? new List<string>()));
            CreateMap<UserDto, User>();
        }
    }
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        { 
            CreateMap<Customer, CustomerDto>()
               .ReverseMap();
        }
    }
}