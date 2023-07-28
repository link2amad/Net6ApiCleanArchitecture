using API.Helpers;
using Application.ServiceInterfaces.ILookupServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LookupController : BaseController
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("GetAllActiveStates")]
        public IActionResult GetAllActiveStates()
        {
            return CreateHttpResponse(() =>
            {
                var response = _lookupService.GetAllActiveStates();
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "State not found" : "",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        [HttpGet("GetAllActiveGenders")]
        public IActionResult GetAllActiveGenders()
        {
            return CreateHttpResponse(() =>
            {
                var response = _lookupService.GetAllActiveGenders();
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "Gender not found" : "",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }
    }
}