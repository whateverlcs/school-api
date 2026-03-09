using Bogus;
using School.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestGetAcademyByStateBuilder
    {
        private string? _state;

        public static RequestGetAcademyByStateBuilder New()
            => new RequestGetAcademyByStateBuilder();

        public RequestGetAcademyByStateBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        public RequestGetAcademyByState Build()
        {
            return new RequestGetAcademyByState
            {
                State = _state!
            };
        }
    }
}