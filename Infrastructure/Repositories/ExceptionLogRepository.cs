using Application.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    internal class ExceptionLogRepository : GenericRepository<ExceptionLog>, IExceptionLogRepository
    {
        public ExceptionLogRepository(AppDbContext context) : base(context)
        {
        }
    }
}