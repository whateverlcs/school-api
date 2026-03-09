using FluentValidation;
using School.Communication.Requests;
using School.Domain.Extensions;
using School.Exceptions;

namespace School.Application.UseCases.Student
{
    public class StudentValidator : AbstractValidator<RequestRegisterStudentJson>
    {
        public StudentValidator()
        {
            RuleFor(student => student.Name).NotEmpty().WithMessage(ResourceMessagesException.STUDENT_NAME_EMPTY);
            RuleFor(student => student.Surname).NotEmpty().WithMessage(ResourceMessagesException.STUDENT_SURNAME_EMPTY);

            RuleFor(student => student.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            When(student => student.Email.NotEmpty(), () =>
            {
                RuleFor(student => student.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });

            RuleFor(student => student.Age).NotNull().WithMessage(ResourceMessagesException.STUDENT_AGE_INVALID);
            RuleFor(student => student.Age)
            .InclusiveBetween(6, 100)
            .WithMessage(ResourceMessagesException.STUDENT_AGE_INVALID);

            RuleFor(student => student.Schooling).IsInEnum().WithMessage(ResourceMessagesException.STUDENT_SCHOOLING_NOT_SUPPORTED);

            RuleFor(x => x.AcademyId)
            .NotEmpty().WithMessage(ResourceMessagesException.STUDENT_ACADEMY_EMPTY);
        }
    }
}