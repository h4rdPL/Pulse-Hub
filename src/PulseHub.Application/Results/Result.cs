namespace PulseHub.Application.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }

        public Result(bool isSuccess, string message, string errorCode = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            ErrorCode = errorCode;
        }

        public static Result Success(string message)
        {
            return new Result(true, message);
        }

        public static Result Failure(string message, string errorCode = null)
        {
            return new Result(false, message, errorCode);
        }
    }
}
