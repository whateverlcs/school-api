using School.Communication.Responses;

namespace School.Application.UseCases.Student.GetByAcademyId
{
    public interface IGetStudentByAcademyIdUseCase
    {
        Task<IList<ResponseStudentJson>> Execute(long academyId);
    }
}