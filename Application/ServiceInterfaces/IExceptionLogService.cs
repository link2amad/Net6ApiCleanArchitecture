using Domain.Entities;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IExceptionLogService
    {
        Task<ExceptionLog> Create(int entityID, string entityName, string method, string json, string RequestUrl, string requestBodyJson, string exception);
    }
}