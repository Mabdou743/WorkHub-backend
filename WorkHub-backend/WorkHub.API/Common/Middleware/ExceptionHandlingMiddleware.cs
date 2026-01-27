using Serilog;
using System.Net;
using WorkHub.Domain;

namespace WorkHub.API
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                Log.Warning(ex, "Domain exception occurred");

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new ApiResult<object>
                {
                    IsSuccess = false,
                    Error = new ApiError
                    {
                        Code = "DOMAIN_ERROR",
                        Message = ex.Message
                    }
                });

            }
            catch(Exception ex)
            {
                Log.Error(ex, "Unhandled exception occurred");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new ApiResult<object>
                {
                    IsSuccess = false,
                    Error = new ApiError
                    {
                        Code = "UNEXPECTED_ERROR",
                        Message = "Unexpected error occurred"
                    }
                });
            }
        }
    }
}
