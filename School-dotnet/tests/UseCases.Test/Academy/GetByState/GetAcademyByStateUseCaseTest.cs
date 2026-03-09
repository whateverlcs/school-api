using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Academy.GetByState;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Academy.GetByState
{
    public class GetAcademyByStateUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academyBuilder = AcademyBuilder.New();
            var academy = academyBuilder.Build();
            var academies = academyBuilder.Collection();

            var request = RequestGetAcademyByStateBuilder
                .New()
                .WithState(academy.State)
                .Build();

            var useCase = CreateUseCase(academy, academies);

            var result = await useCase.Execute(request.State);

            result.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(academies.Count);
        }

        [Fact]
        public async Task Error_State_Empty()
        {
            var request = RequestGetAcademyByStateBuilder.New().WithState(string.Empty).Build();

            var academyBuilder = AcademyBuilder.New();
            var academy = academyBuilder.Build();
            var academies = academyBuilder.Collection();

            var useCase = CreateUseCase(academy, academies);

            Func<Task> act = async () => { await useCase.Execute(request.State); };

            (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.ACADEMY_NOT_FOUND));
        }

        private static GetAcademyByStateUseCase CreateUseCase(
            School.Domain.Entities.Academy academy,
            IList<School.Domain.Entities.Academy> academies)
        {
            var mapper = MapperBuilder.Build();
            var repository = new AcademyReadOnlyRepositoryBuilder().GetByState(academy, academies).Build();

            return new GetAcademyByStateUseCase(mapper, repository);
        }
    }
}