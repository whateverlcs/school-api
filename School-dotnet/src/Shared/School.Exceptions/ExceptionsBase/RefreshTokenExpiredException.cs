using System.Net;

namespace School.Exceptions.ExceptionsBase
{
    public class RefreshTokenExpiredException : SchoolException
    {
        public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}