using MapsterMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Academy;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.GetByState
{
    public class GetAcademyByStateUseCase : IGetAcademyByStateUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAcademyReadOnlyRepository _repository;

        public GetAcademyByStateUseCase(IMapper mapper, IAcademyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<ResponseAcademyJson>> Execute(string stateName)
        {
            var academy = await _repository.GetByState(stateName);

            if (academy is null || !academy.Any())
                throw new NotFoundException(ResourceMessagesException.ACADEMY_NOT_FOUND);

            var response = _mapper.Map<IList<ResponseAcademyJson>>(academy);

            return response;
        }
    }
}
