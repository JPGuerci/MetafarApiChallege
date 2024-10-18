using System.Net;

namespace MetafarApiChallege.Infrastructure.Helpers
{
    public static class ExceptionMappings
    {
        public static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodes = new()
        {
            { typeof(InvalidTransferException), HttpStatusCode.BadRequest },
            { typeof(InternalServerException), HttpStatusCode.InternalServerError },
            { typeof(NotFoundException), HttpStatusCode.NotFound },
            { typeof(UnauthorizedAccessException), HttpStatusCode.Forbidden }
            
        };
    }

    public class InvalidTransferException : Exception
    {
        public InvalidTransferException() : base() { }

        public InvalidTransferException(string message) : base(message) { }

        public InvalidTransferException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InternalServerException : Exception
    {
        public InternalServerException() : base() { }

        public InternalServerException(string message) : base(message) { }

        public InternalServerException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}