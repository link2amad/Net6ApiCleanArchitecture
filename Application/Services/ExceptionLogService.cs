using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExceptionLogService : IExceptionLogService
    {
        private readonly IMapper _mapper;
        private readonly IExceptionLogRepository _exceptionLogRepository;

        public ExceptionLogService(IExceptionLogRepository exceptionLogRepository, IMapper mapper)
        {
            _exceptionLogRepository = exceptionLogRepository;
            _mapper = mapper;
        }

        public async Task<ExceptionLog> Create(int entityID, string entityName, string method, string json, string RequestUrl, string requestBodyJson, string exception)
        {
            try
            {
                var exceptionLog = new ExceptionLog
                {
                    EntityID = entityID,
                    EntityName = entityName,
                    JSON = json,
                    RequestUrl = RequestUrl,
                    RequestJSON = requestBodyJson,
                    Method = method,
                    Exception = exception
                };
                await _exceptionLogRepository.Add(exceptionLog);
                _exceptionLogRepository.Complete();
                return exceptionLog;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}