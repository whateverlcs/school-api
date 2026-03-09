using System.Net;

namespace School.Exceptions.ExceptionsBase
{
    public abstract class SchoolException : SystemException
    {
        protected SchoolException(string message) : base(message)
        {
        }

        public abstract IList<string> GetErrorMessages();

        public abstract HttpStatusCode GetStatusCode();
    }
}