using AutoMapper;
using School.Communication.Responses;
using School.Domain.Repositories.Student;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Student.GetById
{
    public class GetStudentByIdUseCase : IGetStudentByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IStudentReadOnlyRepository _repository;

        public GetStudentByIdUseCase(
            IMapper mapper,
            IStudentReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseStudentJson> Execute(long studentId)
        {
            var student = await _repository.GetById(studentId);

            if (student is null)
                throw new NotFoundException(ResourceMessagesException.STUDENT_NOT_FOUND);

            var response = _mapper.Map<ResponseStudentJson>(student);

            return response;
        }
    }
}