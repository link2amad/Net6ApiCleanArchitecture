#region Imports

using System.Net;

#endregion

namespace API.Helpers
{
    public static class APIUtility
    {
        public static SuccessResponseVM GetApiResponseVM(string message, HttpStatusCode statusCode, bool success,
            object result)
        {
            return new SuccessResponseVM
            {
                Message = message,
                StatusCode = statusCode,
                Result = result,
                Success = success
            };
        }
    }
}