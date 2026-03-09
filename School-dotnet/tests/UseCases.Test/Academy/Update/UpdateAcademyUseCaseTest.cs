using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Academy.Update;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Academy.Update
{
    public class UpdateAcademyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var request = RequestRegisterAcademyJsonBuilder.Build();

            var useCase = CreateUseCase(academy);

            Func<Task> act = async () => await useCase.Execute(academy.Id, request);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Error_Academy_NotFound()
        {
            var academy = AcademyBuilder.New().Build();

            var request = RequestRegisterAcademyJsonBuilder.Build();

            var useCase = CreateUseCase(academy);

            Func<Task> act = async () => await useCase.Execute(academyId: 1000, request);

            (await act.Should().ThrowAsync<NotFoundException>())
                .Where(e => e.Message.Equals(ResourceMessagesException.ACADEMY_NOT_FOUND));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var academy = AcademyBuilder.New().Build();
            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase(academy);

            Func<Task> act = async () => await useCase.Execute(academy.Id, request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.GetErrorMessages().Count == 1 &&
                    e.GetErrorMessages().Contains(ResourceMessagesException.ACADEMY_NAME_EMPTY));
        }

        private static UpdateAcademyUseCase CreateUseCase(School.Domain.Entities.Academy academy)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var repository = new AcademyUpdateOnlyRepositoryBuilder().GetById(academy).Build();

            return new UpdateAcademyUseCase(unitOfWork, mapper, repository);
        }
    }
}