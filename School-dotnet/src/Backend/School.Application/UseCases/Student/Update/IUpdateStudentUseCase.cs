using School.Communication.Requests;

namespace School.Application.UseCases.Student.Update
{
    public interface IUpdateStudentUseCase
    {
        Task Execute(long studentId, RequestRegisterStudentJson request);
    }
}