using School.Communication.Responses;

namespace School.Application.UseCases.Academy.GetAllAcademys
{
    public interface IGetAllAcademysUseCase
    {
        Task<IList<ResponseAcademyJson>> Execute();
    }
}