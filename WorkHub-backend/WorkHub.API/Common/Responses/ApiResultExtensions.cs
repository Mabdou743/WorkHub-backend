using WorkHub.Application;

namespace WorkHub.API
{
    public static class ApiResultExtensions
    {
        public static ApiResult<T> ToApiResult<T>(this Result<T> result)
        {
            return new ApiResult<T>
            {
                IsSuccess = result.IsSuccess,
                Data = result.IsSuccess ? result.Value : default,
                Error = result.IsFailure
                    ? new ApiError
                    {
                        Code = result.Error.Code,
                        Message = result.Error.Message
                    }
                    : null
            };
        }

        public static ApiResult<object> ToApiResult(this Result result)
        {
            return new ApiResult<object>
            {
                IsSuccess = result.IsSuccess,
                Error = result.IsFailure
                    ? new ApiError
                    {
                        Code = result.Error.Code,
                        Message = result.Error.Message
                    }
                    : null
            };
        }
    }
}
