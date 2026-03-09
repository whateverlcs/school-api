using AutoMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Academy;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.GetAllAcademys
{
    public class GetAllAcademysUseCase : IGetAllAcademysUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAcademyReadOnlyRepository _repository;

        public GetAllAcademysUseCase(
            IMapper mapper,
            IAcademyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<ResponseAcademyJson>> Execute()
        {
            var academy = await _repository.GetAllAcademys();

            var response = _mapper.Map<IList<ResponseAcademyJson>>(academy);

            return response;
        }
    }
}