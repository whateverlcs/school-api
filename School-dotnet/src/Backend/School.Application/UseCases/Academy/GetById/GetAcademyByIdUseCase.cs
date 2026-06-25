using MapsterMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Academy;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.GetById
{
    public class GetAcademyByIdUseCase : IGetAcademyByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAcademyReadOnlyRepository _repository;

        public GetAcademyByIdUseCase(IMapper mapper, IAcademyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseAcademyJson> Execute(long academyId)
        {
            var academy = await _repository.GetById(academyId);

            if (academy is null)
                throw new NotFoundException(ResourceMessagesException.ACADEMY_NOT_FOUND);

            var response = _mapper.Map<ResponseAcademyJson>(academy);

            return response;
        }
    }
}
