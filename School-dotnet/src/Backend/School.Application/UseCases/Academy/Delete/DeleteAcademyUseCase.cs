using School.Domain.Repositories;
using School.Domain.Repositories.Academy;
using School.Exceptions;
using School.Exceptions.ExceptionsBase;

namespace School.Application.UseCases.Academy.Delete
{
    public class DeleteAcademyUseCase : IDeleteAcademyUseCase
    {
        private readonly IAcademyReadOnlyRepository _repositoryRead;
        private readonly IAcademyWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAcademyUseCase(
            IAcademyReadOnlyRepository repositoryRead,
            IAcademyWriteOnlyRepository repositoryWrite,
            IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long academyId)
        {
            var academy = await _repositoryRead.GetById(academyId);

            if (academy is null)
                throw new NotFoundException(ResourceMessagesException.ACADEMY_NOT_FOUND);

            await _repositoryWrite.Delete(academyId);

            await _unitOfWork.Commit();
        }
    }
}