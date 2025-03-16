namespace PulseHub.Application.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public string? ErrorCode { get; }
        public T? Data { get; }

        private Result(bool isSuccess, string message, T? data = default, string? errorCode = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            ErrorCode = errorCode;
        }

        public static Result<T> Success(T data, string message = "Operation successful.")
            => new(true, message, data);

        public static Result<T> Failure(string message, string? errorCode = null)
            => new(false, message, default, errorCode);
    }
}
