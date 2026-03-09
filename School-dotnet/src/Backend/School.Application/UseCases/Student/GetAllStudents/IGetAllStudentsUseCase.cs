using School.Communication.Responses;

namespace School.Application.UseCases.Student.GetAllStudents
{
    public interface IGetAllStudentsUseCase
    {
        Task<IList<ResponseStudentJson>> Execute();
    }
}