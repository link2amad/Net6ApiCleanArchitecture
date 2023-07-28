using Application.Dto;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces.ILookupServices;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.LookupServices
{
    public class LookupService : ILookupService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public LookupService(IStateRepository stateRepository,
            IGenderRepository genderRepository,
            IMapper mapper)
        {
            _stateRepository = stateRepository;
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public List<LookupDto> GetAllActiveGenders()
        {
            return _genderRepository.GetAllGenders(true).Select(c => new LookupDto
            {
                Key = c.ID,
                Value = c.Value,
            }).ToList();
        }

        public List<LookupDto> GetAllActiveStates()
        {
            return _stateRepository.GetAllStates(true).Select(x => new LookupDto
            {
                Key = x.ID,
                Value = x.Name
            }).ToList();
        }
    }
}