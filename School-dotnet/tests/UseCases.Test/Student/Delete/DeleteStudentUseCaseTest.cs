using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Student.Delete;

namespace UseCases.Test.Student.Delete
{
    public class DeleteStudentUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);

            var useCase = CreateUseCase(student);

            var act = async () => await useCase.Execute(student.Id);

            await act.Should().NotThrowAsync();
        }

        private static DeleteStudentUseCase CreateUseCase(School.Domain.Entities.Student? student = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var writeRepository = StudentWriteOnlyRepositoryBuilder.Build();
            var readRepository = new StudentReadOnlyRepositoryBuilder().GetById(student).Build();

            return new DeleteStudentUseCase(readRepository, writeRepository, unitOfWork);
        }
    }
}