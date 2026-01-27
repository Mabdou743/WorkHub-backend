namespace WorkHub.Application
{
    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("Cannot access Value of a failed result.");

        private Result(T value) : base(true, Error.None)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private Result(Error error) : base(false, error)
        {
            _value = default!;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static new Result<T> Failure(Error error) => new Result<T>(error);
    }

}
