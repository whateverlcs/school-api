using System.Net;

namespace School.Exceptions.ExceptionsBase
{
    public class UnauthorizedException : SchoolException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}