namespace WorkHub.API
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public ApiError? Error { get; init; }
    }
}
