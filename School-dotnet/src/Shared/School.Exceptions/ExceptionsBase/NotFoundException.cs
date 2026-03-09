using System.Net;

namespace School.Exceptions.ExceptionsBase
{
    public class NotFoundException : SchoolException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}