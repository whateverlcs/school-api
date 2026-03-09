using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Academy.GetByCity;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Academy.GetByCity
{
    public class GetAcademyByCityUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academyBuilder = AcademyBuilder.New();

            var academy = academyBuilder.Build();
            var academies = academyBuilder.Collection();

            var request = RequestGetAcademyByCityBuilder
                .New()
                .WithCity(academy.City)
                .Build();

            var useCase = CreateUseCase(academy, academies);

            var result = await useCase.Execute(request.City);

            result.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(academies.Count);
        }

        [Fact]
        public async Task Error_City_Empty()
        {
            var request = RequestGetAcademyByCityBuilder.New().WithCity(string.Empty).Build();

            var academyBuilder = AcademyBuilder.New();
            var academy = academyBuilder.Build();
            var academies = academyBuilder.Collection();

            var useCase = CreateUseCase(academy, academies);

            Func<Task> act = async () => { await useCase.Execute(request.City); };

            (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.ACADEMY_NOT_FOUND));
        }

        private static GetAcademyByCityUseCase CreateUseCase(
            School.Domain.Entities.Academy academy,
            IList<School.Domain.Entities.Academy> academies)
        {
            var mapper = MapperBuilder.Build();
            var repository = new AcademyReadOnlyRepositoryBuilder().GetByCity(academy, academies).Build();

            return new GetAcademyByCityUseCase(mapper, repository);
        }
    }
}