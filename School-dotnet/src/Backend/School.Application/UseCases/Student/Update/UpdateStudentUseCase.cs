using AutoMapper;
using School.Communication.Requests;
using School.Domain.Extensions;
using School.Domain.Repositories;
using School.Domain.Repositories.Student;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Student.Update
{
    public class UpdateStudentUseCase : IUpdateStudentUseCase
    {
        private readonly IStudentUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStudentUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStudentUpdateOnlyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(long studentId, RequestRegisterStudentJson request)
        {
            Validate(request);

            var student = await _repository.GetById(studentId);

            if (student is null)
                throw new NotFoundException(ResourceMessagesException.STUDENT_NOT_FOUND);

            _mapper.Map(request, student);

            _repository.Update(student);

            await _unitOfWork.Commit();
        }

        private static void Validate(RequestRegisterStudentJson request)
        {
            var result = new StudentValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}