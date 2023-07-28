namespace API.Configurations
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}