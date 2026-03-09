using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Student.GetByAcademyId;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Student.GetByAcademyId
{
    public class GetStudentByAcademyIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var students = StudentBuilder.Collection(academy);

            var useCase = CreateUseCase(student, students);

            var result = await useCase.Execute(student.AcademyId);

            result.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(students.Count);
        }

        [Fact]
        public async Task Error_Student_Academy_NotFound()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var students = StudentBuilder.Collection(academy);

            var useCase = CreateUseCase(student, students);

            Func<Task> act = async () => { await useCase.Execute(academyId: 1000); };

            (await act.Should().ThrowAsync<NotFoundException>())
                .Where(e => e.Message.Equals(ResourceMessagesException.STUDENT_ACADEMY_EMPTY));
        }

        private static GetStudentByAcademyIdUseCase CreateUseCase(
            School.Domain.Entities.Student? student,
            IList<School.Domain.Entities.Student> students)
        {
            var mapper = MapperBuilder.Build();
            var repository = new StudentReadOnlyRepositoryBuilder().GetByAcademyId(student, students).Build();

            return new GetStudentByAcademyIdUseCase(mapper, repository);
        }
    }
}