using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Student;
using School.Exceptions;

namespace Validators.Test.Student
{
    public class StudentValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Student_Name_Empty()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.STUDENT_NAME_EMPTY));
        }

        [Fact]
        public void Error_Student_Surname_Empty()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Surname = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.STUDENT_SURNAME_EMPTY));
        }

        [Fact]
        public void Error_Student_Email_Empty()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Student_Email_Invalid()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Email = "email.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Student_Age_Invalid()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Age = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.STUDENT_AGE_INVALID));
        }

        [Fact]
        public void Error_Invalid_Schooling()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.Schooling = (School.Communication.Enums.Schooling)1000;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.STUDENT_SCHOOLING_NOT_SUPPORTED));
        }

        [Fact]
        public void Error_Student_Academy_Name_Empty()
        {
            var validator = new StudentValidator();

            var academy = AcademyBuilder.New().Build();
            var student = StudentBuilder.Build(academy);
            var academyStudentEncodedId = IdEncripterBuilder.Build().Encode(student.AcademyId);
            var request = RequestRegisterStudentJsonBuilder.Build(academyStudentEncodedId);
            request.AcademyId = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.STUDENT_ACADEMY_EMPTY));
        }
    }
}