using Bogus;
using School.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestGetAcademyByCityBuilder
    {
        private string? _city;

        public static RequestGetAcademyByCityBuilder New()
            => new RequestGetAcademyByCityBuilder();

        public RequestGetAcademyByCityBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public RequestGetAcademyByCity Build()
        {
            return new RequestGetAcademyByCity
            {
                City = _city!
            };
        }
    }
}