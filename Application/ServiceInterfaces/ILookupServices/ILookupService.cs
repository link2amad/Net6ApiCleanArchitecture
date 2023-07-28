using Application.Dto;
using System.Collections.Generic;

namespace Application.ServiceInterfaces.ILookupServices
{
    public interface ILookupService
    {
        List<LookupDto> GetAllActiveStates();

        List<LookupDto> GetAllActiveGenders();
    }
}