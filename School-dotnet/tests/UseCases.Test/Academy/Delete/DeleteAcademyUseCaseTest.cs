using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Academy.Delete;

namespace UseCases.Test.Academy.Delete
{
    public class DeleteAcademyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();

            var useCase = CreateUseCase(academy);

            var act = async () => await useCase.Execute(academy.Id);

            await act.Should().NotThrowAsync();
        }

        private static DeleteAcademyUseCase CreateUseCase(School.Domain.Entities.Academy? academy = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var writeRepository = AcademyWriteOnlyRepositoryBuilder.Build();
            var readRepository = new AcademyReadOnlyRepositoryBuilder().GetById(academy).Build();

            return new DeleteAcademyUseCase(readRepository, writeRepository, unitOfWork);
        }
    }
}