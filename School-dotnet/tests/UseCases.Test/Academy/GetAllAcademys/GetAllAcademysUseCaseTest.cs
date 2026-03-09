using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using School.Application.UseCases.Academy.GetAllAcademys;

namespace UseCases.Test.Academy.GetAllAcademys
{
    public class GetAllAcademysUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academies = AcademyBuilder.New().Collection();

            var useCase = CreateUseCase(academies);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(academies.Count);
        }

        private static GetAllAcademysUseCase CreateUseCase(
            IList<School.Domain.Entities.Academy> academies)
        {
            var mapper = MapperBuilder.Build();
            var repository = new AcademyReadOnlyRepositoryBuilder().GetAllAcademys(academies).Build();

            return new GetAllAcademysUseCase(mapper, repository);
        }
    }
}