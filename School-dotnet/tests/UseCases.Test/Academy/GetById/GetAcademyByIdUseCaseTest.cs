using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Academy.GetById;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Academy.GetById
{
    public class GetAcademyByIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();

            var useCase = CreateUseCase(academy);

            var result = await useCase.Execute(academy.Id);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().Be(academy.Name);
            result.Address.Should().Be(academy.Address);
            result.State.Should().Be(academy.State);
            result.City.Should().Be(academy.City);
        }

        [Fact]
        public async Task Error_Academy_NotFound()
        {
            var academy = AcademyBuilder.New().Build();

            var useCase = CreateUseCase(academy);

            Func<Task> act = async () => { await useCase.Execute(academyId: 1000); };

            (await act.Should().ThrowAsync<NotFoundException>())
                .Where(e => e.Message.Equals(ResourceMessagesException.ACADEMY_NOT_FOUND));
        }

        private static GetAcademyByIdUseCase CreateUseCase(
            School.Domain.Entities.Academy? academy = null)
        {
            var mapper = MapperBuilder.Build();
            var repository = new AcademyReadOnlyRepositoryBuilder().GetById(academy).Build();

            return new GetAcademyByIdUseCase(mapper, repository);
        }
    }
}