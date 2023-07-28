using Domain.Entities;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IApiLogService
    {
        Task<ApiLog> Create(ApiLog apiLogVM);
    }
}