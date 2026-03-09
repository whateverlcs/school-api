using School.Communication.Responses;

namespace School.Application.UseCases.Student.GetById
{
    public interface IGetStudentByIdUseCase
    {
        Task<ResponseStudentJson> Execute(long studentId);
    }
}