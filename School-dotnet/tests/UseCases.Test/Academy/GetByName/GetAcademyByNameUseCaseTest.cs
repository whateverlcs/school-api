using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Academy.GetByName;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Academy.GetByName
{
    public class GetAcademyByNameUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academyBuilder = AcademyBuilder.New();
            var academy = academyBuilder.Build();
            var academies = academyBuilder.Collection();

            var request = RequestGetAcademyByNameBuilder
                .New()
                .WithName(academy.Name)
                .Build();

            var useCase = CreateUseCase(academy, academies);

            var result = await useCase.Execute(request.Name);

            result.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(academies.Count);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestGetAcademyByNameBuilder.New().WithName(string.Empty).Build();
            request.Name = string.Empty;

            var academyBuilder = AcademyBuilder.New();
            var academy = academyBuilder.Build();
            var academies = academyBuilder.Collection();

            var useCase = CreateUseCase(academy, academies);

            Func<Task> act = async () => { await useCase.Execute(request.Name); };

            (await act.Should().ThrowAsync<NotFoundException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.ACADEMY_NOT_FOUND));
        }

        private static GetAcademyByNameUseCase CreateUseCase(
            School.Domain.Entities.Academy academy,
            IList<School.Domain.Entities.Academy> academies)
        {
            var mapper = MapperBuilder.Build();
            var repository = new AcademyReadOnlyRepositoryBuilder().GetByName(academy, academies).Build();

            return new GetAcademyByNameUseCase(mapper, repository);
        }
    }
}