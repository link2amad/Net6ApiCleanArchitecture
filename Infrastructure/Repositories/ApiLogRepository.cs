using Application.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    internal class ApiLogRepository : GenericRepository<ApiLog>, IApiLogRepository
    {
        public ApiLogRepository(AppDbContext context) : base(context)
        {
        }
    }
}