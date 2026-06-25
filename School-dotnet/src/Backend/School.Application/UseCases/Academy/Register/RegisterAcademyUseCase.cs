using MapsterMapper;
using School.Communication.Requests;
using School.Communication.Responses;
using School.Domain.Extensions;
using School.Domain.Repositories;
using School.Domain.Repositories.Academy;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.Register
{
    public class RegisterAcademyUseCase : IRegisterAcademyUseCase
    {
        private readonly IAcademyWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterAcademyUseCase(
            IAcademyWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseAcademyJson> Execute(RequestRegisterAcademyJson request)
        {
            Validate(request);

            var academy = _mapper.Map<Domain.Entities.Academy>(request);

            await _repository.Add(academy);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseAcademyJson>(academy);
        }

        private static void Validate(RequestRegisterAcademyJson request)
        {
            var result = new AcademyValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList()
                );
        }
    }
}
