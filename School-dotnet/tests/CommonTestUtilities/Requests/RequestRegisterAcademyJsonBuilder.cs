using Bogus;
using School.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterAcademyJsonBuilder
    {
        public static RequestRegisterAcademyJson Build()
        {
            return new Faker<RequestRegisterAcademyJson>()
                .RuleFor(student => student.Name, (f) => f.Person.FirstName)
                .RuleFor(student => student.Address, (f) => $"{f.Person.Address.Street}, {f.Person.Address.Suite}")
                .RuleFor(student => student.State, (f) => f.Person.Address.State)
                .RuleFor(student => student.City, (f) => f.Person.Address.City);
        }
    }
}