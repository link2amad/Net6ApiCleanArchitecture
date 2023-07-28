using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ApiLogService : IApiLogService
    {
        private readonly IMapper _mapper;
        private readonly IApiLogRepository _apiLogRepository;

        public ApiLogService(IApiLogRepository apiLogRepository, IMapper mapper)
        {
            _apiLogRepository = apiLogRepository;
            _mapper = mapper;
        }

        public async Task<ApiLog> Create(ApiLog apiLog)
        {
            try
            {
                await _apiLogRepository.Add(apiLog);
                _apiLogRepository.Complete();
                return apiLog;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}