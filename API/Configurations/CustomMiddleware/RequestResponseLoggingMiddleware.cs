using Application.ServiceInterfaces;
using AutoMapper;
using Domain.Entities;
using System.Text;

namespace API.Configurations.CustomMiddleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiLogService apiLogService, IMapper mapper)
        {
            //First, get the incoming request
            var apiLog = await FormatRequest(context);
            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                apiLog.Response = await FormatResponse(context.Response);
                apiLog.ResponseAt = DateTime.UtcNow;
                apiLog.ResponseStatusCode = context.Response.StatusCode;

                //TODO: Save log to chosen datastore
                await apiLogService.Create(apiLog);
                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<ApiLog> FormatRequest(HttpContext context)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            context.Request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            context.Request.Body.Position = 0;

            var logVM = new ApiLog
            {
                RequestAt = DateTime.UtcNow,
                RequestURL = $"{context.Request.Scheme}://{ context.Request.Host }{ context.Request.Path } { context.Request.QueryString }".Trim(),
                RequestByURL = context.Request.Headers["Referer"].ToString(),
                IPAddress = context.Connection.RemoteIpAddress?.ToString(),
                RequestBody = bodyAsText
            };
            context.Items["RequestLog"] = logVM;
            return logVM;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return text;
        }
    }
}