using CommonTestUtilities.Requests;
using FluentAssertions;
using School.Application.UseCases.Academy;
using School.Exceptions;

namespace Validators.Test.Academy
{
    public class AcademyValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new AcademyValidator();

            var request = RequestRegisterAcademyJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Academy_Name_Empty()
        {
            var validator = new AcademyValidator();

            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.ACADEMY_NAME_EMPTY));
        }

        [Fact]
        public void Error_Academy_Address_Empty()
        {
            var validator = new AcademyValidator();

            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.Address = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.ACADEMY_ADDRESS_EMPTY));
        }

        [Fact]
        public void Error_Academy_Address_Too_Long()
        {
            var validator = new AcademyValidator();

            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.Address = RequestStringGenerator.Paragraphs(minCharacters: 256);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.ACADEMY_ADDRESS_EXCEEDS_LIMIT_CHARACTERS));
        }

        [Fact]
        public void Error_Academy_State_Empty()
        {
            var validator = new AcademyValidator();

            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.State = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.ACADEMY_STATE_EMPTY));
        }

        [Fact]
        public void Error_Academy_City_Empty()
        {
            var validator = new AcademyValidator();

            var request = RequestRegisterAcademyJsonBuilder.Build();
            request.City = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.ACADEMY_CITY_EMPTY));
        }
    }
}