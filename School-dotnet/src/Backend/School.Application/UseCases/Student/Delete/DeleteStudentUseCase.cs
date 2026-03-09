using School.Domain.Repositories;
using School.Domain.Repositories.Student;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Student.Delete
{
    public class DeleteStudentUseCase : IDeleteStudentUseCase
    {
        private readonly IStudentReadOnlyRepository _repositoryRead;
        private readonly IStudentWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStudentUseCase(
            IStudentReadOnlyRepository repositoryRead,
            IStudentWriteOnlyRepository repositoryWrite,
            IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long studentId)
        {
            var student = await _repositoryRead.GetById(studentId);

            if (student is null)
                throw new NotFoundException(ResourceMessagesException.STUDENT_NOT_FOUND);

            await _repositoryWrite.Delete(studentId);

            await _unitOfWork.Commit();
        }
    }
}