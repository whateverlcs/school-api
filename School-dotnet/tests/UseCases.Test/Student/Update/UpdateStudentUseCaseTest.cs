using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Student.Update;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace UseCases.Test.Student.Update
{
    public class UpdateStudentUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);

            var useCase = CreateUseCase(student);

            Func<Task> act = async () => await useCase.Execute(student.Id, request);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Error_Student_NotFound()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);

            var useCase = CreateUseCase(student);

            Func<Task> act = async () => await useCase.Execute(studentId: 1000, request);

            (await act.Should().ThrowAsync<NotFoundException>())
                .Where(e => e.Message.Equals(ResourceMessagesException.STUDENT_NOT_FOUND));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Name = string.Empty;

            var useCase = CreateUseCase(student);

            Func<Task> act = async () => await useCase.Execute(student.Id, request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.GetErrorMessages().Count == 1 &&
                    e.GetErrorMessages().Contains(ResourceMessagesException.STUDENT_NAME_EMPTY));
        }

        private static UpdateStudentUseCase CreateUseCase(School.Domain.Entities.Student student)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var repository = new StudentUpdateOnlyRepositoryBuilder().GetById(student).Build();

            return new UpdateStudentUseCase(unitOfWork, mapper, repository);
        }
    }
}