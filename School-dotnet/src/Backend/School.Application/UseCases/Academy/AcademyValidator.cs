using FluentValidation;
using School.Communication.Requests;
using School.Exceptions;

namespace School.Application.UseCases.Academy
{
    public class AcademyValidator : AbstractValidator<RequestRegisterAcademyJson>
    {
        public AcademyValidator()
        {
            RuleFor(academy => academy.Name).NotEmpty().WithMessage(ResourceMessagesException.ACADEMY_NAME_EMPTY);
            RuleFor(academy => academy.Address)
                .NotEmpty().WithMessage(ResourceMessagesException.ACADEMY_ADDRESS_EMPTY)
                .MaximumLength(255)
                .WithMessage(ResourceMessagesException.ACADEMY_ADDRESS_EXCEEDS_LIMIT_CHARACTERS);
            RuleFor(academy => academy.State).NotEmpty().WithMessage(ResourceMessagesException.ACADEMY_STATE_EMPTY);
            RuleFor(academy => academy.City).NotEmpty().WithMessage(ResourceMessagesException.ACADEMY_CITY_EMPTY);
        }
    }
}