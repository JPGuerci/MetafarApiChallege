using System.Net;

namespace MetafarApiChallege.Infrastructure.Helpers
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public CustomException(HttpStatusCode statusCode, string? message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
    public class AccountNotFoundException : CustomException
    {
        public AccountNotFoundException(string message)
            : base(HttpStatusCode.NotFound, message) { }
    }

    public class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException(string message)
            : base(HttpStatusCode.Unauthorized, message) { }
    }

    public class DatabaseException : CustomException
    {
        public DatabaseException(string message)
            : base(HttpStatusCode.InternalServerError, message) { }
    }
    public class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException(string message)
            : base(HttpStatusCode.BadRequest, message) { }
    }
    public class BusinessException : CustomException
    {
        public BusinessException(string message)
            : base(HttpStatusCode.BadRequest, message) { }
    }
}
