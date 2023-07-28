using API.Helpers;
using Application.Dto;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(
            ICustomerService CustomerService
        )
        {
            _customerService = CustomerService;
        }

        #region Customer

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "Customer")]
        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            return CreateHttpResponse(() =>
            {
                var response = _customerService.GetCustomer(id).Result;
                var isSuccess = response != null;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = isSuccess ? "Customer got successfully" : "No Customer found at given id",
                    Result = isSuccess ? response : null,
                    StatusCode = HttpStatusCode.OK,
                    Success = isSuccess
                });
            });
        }

        /// <summary>
        ///     Upsert a Customer
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "Customer")]
        [HttpPost("UpsertCustomer")]
        public async Task<IActionResult> UpsertCustomer(CustomerDto Customer)
        {
            return CreateHttpResponse(() =>
            {
                var response = _customerService.UpsertCustomer(Customer).Result;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "Customer not found" : "Customer Upserted successfully",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        /// <summary>
        ///     Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "Customer")]
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            return CreateHttpResponse(() =>
            {
                var response = _customerService.DeleteCustomer(id).Result;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "Customer not found" : "Customer removed successfully",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        /// <summary>
        ///     Get Customers
        /// </summary>
        [ApiExplorerSettings(GroupName = "Customer")]
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            return CreateHttpResponse(() =>
            {
                var response = _customerService.GetAllCustomers().Result;
                return new OkObjectResult(new SuccessResponseVM
                {
                    Message = response == null ? "Customer not found" : $"{response.Count()} Customer(s) found.",
                    Result = response,
                    StatusCode = HttpStatusCode.OK,
                    Success = response != null
                });
            });
        }

        #endregion Customer
    }
}