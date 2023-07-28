#region Imports

using Application.Dto;
using Application.Dto;
using Application.Mapper;
using Application.ServiceInterfaces;
using Application.ServiceInterfaces.IGeneralServices;
using Application.ServiceInterfaces.ILookupServices;
using Application.ServiceInterfaces.IUserServices;
using Application.Services;
using Application.Services.GeneralServices;
using Application.Services.LookupServices;
using Application.Services.UserServices;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

#endregion

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IApiLogService, ApiLogService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IExceptionLogService, ExceptionLogService>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISystemSettingService, SystemSettingService>();

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfiles(new List<Profile>() { new MapperProfile(), new CustomerMapperProfile() }); });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(
            fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CustomerValidator>(lifetime: ServiceLifetime.Scoped, includeInternalTypes: true);
                fv.RegisterValidatorsFromAssemblyContaining<UserUpsertDtoValidator>(lifetime: ServiceLifetime.Scoped, includeInternalTypes: true);
                fv.ImplicitlyValidateChildProperties = true;
            });
            return services;
        }
    }
}