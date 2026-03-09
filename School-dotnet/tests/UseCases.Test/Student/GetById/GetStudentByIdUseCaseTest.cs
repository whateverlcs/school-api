using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Student.GetById;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Student.GetById
{
    public class GetStudentByIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);

            var useCase = CreateUseCase(student);

            var result = await useCase.Execute(student.Id);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().Be(student.Name);
            result.Surname.Should().Be(student.Surname);
            result.Email.Should().Be(student.Email);
            result.Age.Should().Be(student.Age);
        }

        [Fact]
        public async Task Error_Student_NotFound()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);

            var useCase = CreateUseCase(student);

            Func<Task> act = async () => { await useCase.Execute(studentId: 1000); };

            (await act.Should().ThrowAsync<NotFoundException>())
                .Where(e => e.Message.Equals(ResourceMessagesException.STUDENT_NOT_FOUND));
        }

        private static GetStudentByIdUseCase CreateUseCase(
            School.Domain.Entities.Student? student = null)
        {
            var mapper = MapperBuilder.Build();
            var repository = new StudentReadOnlyRepositoryBuilder().GetById(student).Build();

            return new GetStudentByIdUseCase(mapper, repository);
        }
    }
}