using Bogus;
using School.Domain.Entities;
using School.Domain.Enums;

namespace CommonTestUtilities.Entities
{
    public class StudentBuilder
    {
        public static IList<Student> Collection(Academy academy, uint count = 2)
        {
            var list = new List<Student>();

            if (count == 0)
                count = 1;

            var studentId = 1;

            for (int i = 0; i < count; i++)
            {
                var fakeStudent = Build(academy);
                fakeStudent.Id = studentId++;

                list.Add(fakeStudent);
            }

            return list;
        }

        public static Student Build(Academy academy)
        {
            return new Faker<Student>()
                .RuleFor(student => student.Id, () => 1)
                .RuleFor(student => student.Name, (f) => f.Person.FirstName)
                .RuleFor(student => student.Surname, (f) => f.Person.LastName)
                .RuleFor(student => student.Email, (f, student) => f.Internet.Email(student.Name))
                .RuleFor(student => student.Age, (f) => f.Random.Number(8, 90))
                .RuleFor(student => student.Schooling, (f) => f.PickRandom<Schooling>())
                .RuleFor(r => r.AcademyId, _ => academy.Id);
        }
    }
}