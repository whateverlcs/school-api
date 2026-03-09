using Bogus;
using School.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestGetAcademyByNameBuilder
    {
        private string? _name;

        public static RequestGetAcademyByNameBuilder New()
            => new RequestGetAcademyByNameBuilder();

        public RequestGetAcademyByNameBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public RequestGetAcademyByName Build()
        {
            return new RequestGetAcademyByName
            {
                Name = _name!
            };
        }
    }
}