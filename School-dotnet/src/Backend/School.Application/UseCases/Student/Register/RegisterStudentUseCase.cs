using MapsterMapper;
using School.Communication.Requests;
using School.Communication.Responses;
using School.Domain.Extensions;
using School.Domain.Repositories;
using School.Domain.Repositories.Student;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Student.Register
{
    public class RegisterStudentUseCase : IRegisterStudentUseCase
    {
        private readonly IStudentWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterStudentUseCase(
            IStudentWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseStudentJson> Execute(RequestRegisterStudentJson request)
        {
            Validate(request);

            var student = _mapper.Map<Domain.Entities.Student>(request);

            await _repository.Add(student);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseStudentJson>(student);
        }

        private static void Validate(RequestRegisterStudentJson request)
        {
            var result = new StudentValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList()
                );
        }
    }
}
