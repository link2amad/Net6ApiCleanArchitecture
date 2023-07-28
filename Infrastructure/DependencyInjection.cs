#region Imports

using Application.ExternalDependencies;
using Application.RepositoryInterfaces;
using Infrastructure.ExternalDependenciesImplementation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Repositories.GeneralRepositories;
using Infrastructure.Repositories.LookupRepositories;
using Infrastructure.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IApiLogRepository, ApiLogRepository>();
            services.AddTransient<IExceptionLogRepository, ExceptionLogRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddTransient<ISystemSettingRepository, SystemSettingRepository>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddSingleton<IEmailHandler, EmailHandler>();
            services.AddMemoryCache();
            services.AddDbContext<AppDbContext>(opt => {
                opt
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}