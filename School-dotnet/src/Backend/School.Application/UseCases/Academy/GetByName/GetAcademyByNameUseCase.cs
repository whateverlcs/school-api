using MapsterMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Academy;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.GetByName
{
    public class GetAcademyByNameUseCase : IGetAcademyByNameUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAcademyReadOnlyRepository _repository;

        public GetAcademyByNameUseCase(IMapper mapper, IAcademyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<ResponseAcademyJson>> Execute(string academyName)
        {
            var academy = await _repository.GetByName(academyName);

            if (academy is null || !academy.Any())
                throw new NotFoundException(ResourceMessagesException.ACADEMY_NOT_FOUND);

            var response = _mapper.Map<IList<ResponseAcademyJson>>(academy);

            return response;
        }
    }
}
