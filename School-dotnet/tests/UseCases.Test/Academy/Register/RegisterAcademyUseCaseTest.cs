using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Academy.Register;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Academy.Register
{
    public class RegisterAcademyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterAcademyJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().Be(request.Name);
            result.Address.Should().Be(request.Address);
            result.State.Should().Be(request.State);
            result.City.Should().Be(request.City);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesException.ACADEMY_NAME_EMPTY));
        }

        private static RegisterAcademyUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var writeRepository = AcademyWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterAcademyUseCase(writeRepository, unitOfWork, mapper);
        }
    }
}