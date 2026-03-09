using AutoMapper;
using School.Communication.Requests;
using School.Domain.Extensions;
using School.Domain.Repositories;
using School.Domain.Repositories.Academy;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.Update
{
    public class UpdateAcademyUseCase : IUpdateAcademyUseCase
    {
        private readonly IAcademyUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAcademyUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAcademyUpdateOnlyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(long academyId, RequestRegisterAcademyJson request)
        {
            Validate(request);

            var academy = await _repository.GetById(academyId);

            if (academy is null)
                throw new NotFoundException(ResourceMessagesException.ACADEMY_NOT_FOUND);

            _mapper.Map(request, academy);

            _repository.Update(academy);

            await _unitOfWork.Commit();
        }

        private static void Validate(RequestRegisterAcademyJson request)
        {
            var result = new AcademyValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}