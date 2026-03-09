using Bogus;
using School.Communication.Enums;
using School.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterStudentJsonBuilder
    {
        public static RequestRegisterStudentJson Build(string academyId)
        {
            return new Faker<RequestRegisterStudentJson>()
                .RuleFor(student => student.Name, (f) => f.Person.FirstName)
                .RuleFor(student => student.Surname, (f) => f.Person.LastName)
                .RuleFor(student => student.Email, (f, student) => f.Internet.Email(student.Name))
                .RuleFor(student => student.Age, (f) => f.Random.Number(8, 90))
                .RuleFor(student => student.Schooling, (f) => f.PickRandom<Schooling>())
                .RuleFor(r => r.AcademyId, _ => academyId);
        }
    }
}