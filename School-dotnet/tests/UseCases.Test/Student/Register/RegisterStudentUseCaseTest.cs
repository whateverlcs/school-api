using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Student.Register;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Student.Register
{
    public class RegisterStudentUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().Be(request.Name);
            result.Surname.Should().Be(request.Surname);
            result.Email.Should().Be(request.Email);
            result.Age.Should().Be(request.Age);
            result.Schooling.Should().Be(request.Schooling);
            result.AcademyId.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesException.STUDENT_NAME_EMPTY));
        }

        private static RegisterStudentUseCase CreateUseCase()
        {
            var mapper = MapperBuilder.Build();
            var writeRepository = StudentWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterStudentUseCase(writeRepository, unitOfWork, mapper);
        }
    }
}