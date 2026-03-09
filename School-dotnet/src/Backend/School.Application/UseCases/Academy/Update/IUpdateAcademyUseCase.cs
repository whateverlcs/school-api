using School.Communication.Requests;

namespace School.Application.UseCases.Academy.Update
{
    public interface IUpdateAcademyUseCase
    {
        Task Execute(long academyId, RequestRegisterAcademyJson request);
    }
}