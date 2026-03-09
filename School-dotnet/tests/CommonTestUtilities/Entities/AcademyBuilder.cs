using Bogus;
using School.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class AcademyBuilder
    {
        private readonly Faker _faker = new();

        private string? _city;
        private string? _state;
        private string? _name;

        public static AcademyBuilder New()
            => new AcademyBuilder();

        public Academy Build()
        {
            return new Faker<Academy>()
                .RuleFor(a => a.Id, _ => 1)
                .RuleFor(a => a.Name, _ => _name ??= _faker.Company.CompanyName())
                .RuleFor(a => a.State, _ => _state ??= _faker.Address.State())
                .RuleFor(a => a.City, _ => _city ??= _faker.Address.City());
        }

        public IList<Academy> Collection(uint count = 2)
        {
            var list = new List<Academy>();

            for (int i = 0; i < count; i++)
            {
                var academy = Build();
                academy.Id = i + 1;
                list.Add(academy);
            }

            return list;
        }
    }
}
