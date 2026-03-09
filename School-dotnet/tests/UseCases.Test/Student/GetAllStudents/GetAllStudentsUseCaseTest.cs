using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Student.GetAllStudents;

namespace UseCases.Test.Student.GetAllStudents
{
    public class GetAllStudentsUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var students = StudentBuilder.Collection(academy);

            var useCase = CreateUseCase(students);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(students.Count);
        }

        private static GetAllStudentsUseCase CreateUseCase(
            IList<School.Domain.Entities.Student> students)
        {
            var mapper = MapperBuilder.Build();
            var repository = new StudentReadOnlyRepositoryBuilder().GetAllStudents(students).Build();

            return new GetAllStudentsUseCase(mapper, repository);
        }
    }
}