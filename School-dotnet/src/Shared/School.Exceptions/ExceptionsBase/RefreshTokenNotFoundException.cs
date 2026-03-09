using System.Net;

namespace School.Exceptions.ExceptionsBase
{
    public class RefreshTokenNotFoundException : SchoolException
    {
        public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}