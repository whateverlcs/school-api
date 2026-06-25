using MapsterMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Student;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Student.GetByAcademyId
{
    public class GetStudentByAcademyIdUseCase : IGetStudentByAcademyIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IStudentReadOnlyRepository _repository;

        public GetStudentByAcademyIdUseCase(IMapper mapper, IStudentReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<ResponseStudentJson>> Execute(long academyId)
        {
            var student = await _repository.GetByAcademyId(academyId);

            if (student is null || !student.Any())
                throw new NotFoundException(ResourceMessagesException.STUDENT_ACADEMY_EMPTY);

            var response = _mapper.Map<IList<ResponseStudentJson>>(student);

            return response;
        }
    }
}
