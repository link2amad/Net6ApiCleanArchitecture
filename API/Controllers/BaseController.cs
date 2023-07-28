#region Imports

using API.Helpers;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Swashbuckle.Swagger.Annotations;
using System.Net;

#endregion

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[SwaggerResponse(HttpStatusCode.OK, Description = "Request processed successfully.")] //200
    //[SwaggerResponse(HttpStatusCode.BadRequest,
    //    Description = "An invalid or missing input parameter will result in a bad request.")] //400
    //[SwaggerResponse(HttpStatusCode.Unauthorized, Description = "Authentication failed.")] //401
    //[SwaggerResponse(HttpStatusCode.NotFound, Description = "No record found.")] //404
    //[SwaggerResponse(HttpStatusCode.InternalServerError, Description = "An unexpected error occurred.")] //500
    public class BaseController : ControllerBase
    {
        protected TimeSpan _datetimeToUserDateTimeSpan = TimeSpan.FromMinutes(0);

        protected BaseController()
        {
        }

        #region BaseAPIMethod for API Version Info

        //[ApiExplorerSettings(IgnoreApi = true)]
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var rng = new Random();
        //    IEnumerable<object> response = Enumerable.Range(1, 1).Select(index => new
        //    {
        //        Title = "ASU API",
        //        Summary = "ASU API Basic v1.0",
        //        Date = DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString() + " | UTC"
        //    })
        //        .ToArray();

        //    return Ok(response);
        //}

        #endregion

        protected ObjectResult CreateHttpResponse(Func<ObjectResult> function)
        {
            ObjectResult response = null;
            try
            {
                if (ModelState.IsValid)
                    response = function.Invoke();
                else
                    response = new BadRequestObjectResult(new SuccessResponseVM
                    {
                        Message = "Validation Errors",
                        Result = ModelState.Values.SelectMany(values =>
                            values.Errors.Select(error => error.ErrorMessage)),
                        StatusCode = HttpStatusCode.BadRequest,
                        Success = false
                    });
            }
            catch (BadRequestException ex)
            {
                response = new BadRequestObjectResult(new SuccessResponseVM
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = "",
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                });
            }
            catch (DbEntityValidationException ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{
                //    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                //    }
                //}
                //LogError(ex);
                response = new InternalServerErrorObjectResult(new SuccessResponseVM
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = ex.StackTrace,
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                });
            }
            catch (DbUpdateException dbEx)
            {
                response = new InternalServerErrorObjectResult(new SuccessResponseVM
                {
                    Message = dbEx.InnerException?.Message ?? dbEx.Message,
                    Result = dbEx.StackTrace,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                });
            }
            catch (Exception ex)
            {
                response = new InternalServerErrorObjectResult(new SuccessResponseVM
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = ex.StackTrace,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                });
            }

            return response;
        }
    }
}