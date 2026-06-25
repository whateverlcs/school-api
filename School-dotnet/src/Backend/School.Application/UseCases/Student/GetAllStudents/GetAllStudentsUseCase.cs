using MapsterMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Student;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Student.GetAllStudents
{
    public class GetAllStudentsUseCase : IGetAllStudentsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IStudentReadOnlyRepository _repository;

        public GetAllStudentsUseCase(IMapper mapper, IStudentReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<ResponseStudentJson>> Execute()
        {
            var student = await _repository.GetAllStudents();

            var response = _mapper.Map<IList<ResponseStudentJson>>(student);

            return response;
        }
    }
}
